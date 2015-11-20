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
    //[Authorize(Roles = "Admin")]
    [RoutePrefix("api/persons")]
    public class PersonsController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Person> _personRepository;

        public PersonsController(IEntityBaseRepository<Person> personRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _personRepository = personRepository;
        }
        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? id, string filter)
        {
            filter = filter.ToLower().Trim();

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var persons = _personRepository.FindBy(p => p.FamilyId == id && p.Deleted == false)
                    .Where(p => p.FirstName.ToLower().Contains(filter) ||
                    p.SurName.ToLower().Contains(filter)).ToList();

                var personsVm = Mapper.Map<IEnumerable<Person>, IEnumerable<PersonViewModel>>(persons);

                response = request.CreateResponse<IEnumerable<PersonViewModel>>(HttpStatusCode.OK, personsVm);

                return response;
            });
        }


        [HttpGet]
        [Route("search/{id:int}/{page:int=0}/{pageSize=10}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int id, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Person> persons = null;
                int totalPersons = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();

                    persons = _personRepository.FindBy(c => (c.SurName.ToLower().Contains(filter.ToLower())
                                                          || c.FirstName.ToLower().Contains(filter.ToLower())) && (c.FamilyId == id) && c.Deleted == false)
                        .OrderBy(c => c.SurName)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalPersons = _personRepository
                        .FindBy(c => (c.SurName.ToLower().Contains(filter.ToLower())
                             || c.FirstName.ToLower().Contains(filter.ToLower())) && c.Deleted == false)
                        .Where(c => (c.SurName.ToLower().Contains(filter.ToLower())
                             || c.FirstName.ToLower().Contains(filter.ToLower())) && c.Deleted == false)
                        .Count();
                }
                else
                {
                    persons = _personRepository
                        .FindBy(c => c.FamilyId == id && c.Deleted == false)
                        .OrderBy(c => c.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalPersons = _personRepository
                        .FindBy(c => c.FamilyId == id && c.Deleted == false).Count();
                }

                IEnumerable<PersonViewModel> personsVM = Mapper.Map<IEnumerable<Person>, IEnumerable<PersonViewModel>>(persons);

                PaginationSet<PersonViewModel> pagedSet = new PaginationSet<PersonViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalPersons,
                    TotalPages = (int)Math.Ceiling((decimal)totalPersons / currentPageSize),
                    Items = personsVM
                };

                response = request.CreateResponse<PaginationSet<PersonViewModel>>(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var person = _personRepository.GetSingle(id);

                PersonViewModel personVM = Mapper.Map<Person, PersonViewModel>(person);

                response = request.CreateResponse<PersonViewModel>(HttpStatusCode.OK, personVM);

                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, PersonViewModel person)
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
                    Person _person = _personRepository.GetSingle(person.ID);
                    _person.UpdatePerson(person);
                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);

                }

                return response;
            });
        }

        [HttpPost]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, PersonViewModel person)
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
                    var personDb = _personRepository.GetSingle(person.ID);
                    if (personDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid person.");
                    else
                    {
                        personDb.UpdatePerson(person);
                        _personRepository.Delete(personDb);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<PersonViewModel>(HttpStatusCode.OK, person);
                    }
                }

                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, PersonViewModel person)
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
                    Person newPerson = new Person();
                    newPerson.UpdatePerson(person);

                    _personRepository.Add(newPerson);

                    _unitOfWork.Commit();

                    // Update view model
                    person = Mapper.Map<Person, PersonViewModel>(newPerson);
                    response = request.CreateResponse<PersonViewModel>(HttpStatusCode.Created, person);
                }

                return response;
            });
        }

    }
}