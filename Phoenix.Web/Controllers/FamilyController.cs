using AutoMapper;
using Phoenix.Data.Infrastructure;
using Phoenix.Data.Repositories;
using Phoenix.Entities;
using Phoenix.Web.Infrastructure.Core;
using Phoenix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Phoenix.Web.Infrastructure.Extensions;
using Phoenix.Data.Extensions;

namespace Phoenix.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/families")]
    public class FamilyController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Family> _familyRepository;

        public FamilyController(IEntityBaseRepository<Family> familyRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _familyRepository = familyRepository;
        }

        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=10}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Family> families = null;
                int totalFamilies = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    families = _familyRepository.FindBy(c => c.FamilyName.ToLower().Contains(filter))
                        .OrderBy(c => c.FamilyName)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalFamilies = _familyRepository.GetAll()
                        .Where(c => c.FamilyName.ToLower().Contains(filter))
                        .Count();
                }
                else
                {
                    families = _familyRepository.GetAll()
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalFamilies = _familyRepository.GetAll().Count();
                }

                IEnumerable<FamilyViewModel> familiesVM = Mapper.Map<IEnumerable<Family>, IEnumerable<FamilyViewModel>>(families);

                PaginationSet<FamilyViewModel> pagedSet = new PaginationSet<FamilyViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalFamilies,
                    TotalPages = (int)Math.Ceiling((decimal)totalFamilies / currentPageSize),
                    Items = familiesVM
                };

                response = request.CreateResponse<PaginationSet<FamilyViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var family = _familyRepository.GetSingle(id);

                FamilyViewModel familyVm = Mapper.Map<Family, FamilyViewModel>(family);

                response = request.CreateResponse<FamilyViewModel>(HttpStatusCode.OK, familyVm);

                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, FamilyViewModel family)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    Family _family = _familyRepository.GetSingle(family.ID);
                    _family.UpdateFamily(family);

                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [HttpPost]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, FamilyViewModel family)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    var familyDb = _familyRepository.GetSingle(family.ID);
                    if (familyDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid family.");
                    else
                    {
                        familyDb.UpdateFamily(family);
                        _familyRepository.Delete(familyDb);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<FamilyViewModel>(HttpStatusCode.OK, family);
                    }
                }

                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, FamilyViewModel family)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    if (_familyRepository.FamilyExists(family.FamilyName))
                    {
                        ModelState.AddModelError("Error", "This family name already exists");
                        response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                    }
                    else
                    {
                        Family newFamily = new Family();
                        newFamily.UpdateFamily(family);
                        _familyRepository.Add(newFamily);

                        _unitOfWork.Commit();

                        // Update view model
                        family = Mapper.Map<Family, FamilyViewModel>(newFamily);
                        response = request.CreateResponse<FamilyViewModel>(HttpStatusCode.Created, family);
                    }
                }

                return response;
            });
        }

    }
}