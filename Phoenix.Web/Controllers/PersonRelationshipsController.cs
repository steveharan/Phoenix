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
    [RoutePrefix("api/personRelationships")]
    public class PersosRelationshipController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<PersonRelationship> _personRelationshipRepository;

        public PersosRelationshipController(IEntityBaseRepository<PersonRelationship> personRelationshipRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _personRelationshipRepository = personRelationshipRepository;
        }
        /// <summary>
        /// Get relationships for a given person
        /// </summary>
        /// <param name="request">The request object</param>
        /// <param name="id">Person id, to get relationships for</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var personRelationships = _personRelationshipRepository.FindBy(p => p.PersonId == id).ToList();

                var personsRelationshipsVm = Mapper.Map<IEnumerable<PersonRelationship>, IEnumerable<PersonRelationshipViewModel>>(personRelationships);

                response = request.CreateResponse<IEnumerable<PersonRelationshipViewModel>>(HttpStatusCode.OK, personsRelationshipsVm);

                return response;
            });
        }

        /// <summary>
        /// Get a single relationship row
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="id">Relationship Id</param>
        /// <returns></returns>
        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var personRelationship = _personRelationshipRepository.GetSingle(id);

                PersonRelationshipViewModel personRelationshipVM = Mapper.Map<PersonRelationship, PersonRelationshipViewModel>(personRelationship);

                response = request.CreateResponse<PersonRelationshipViewModel>(HttpStatusCode.OK, personRelationshipVM);

                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, PersonRelationshipViewModel personRelationship)
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
                    PersonRelationship _personRelationship = _personRelationshipRepository.GetSingle(personRelationship.ID);
                    _personRelationship.UpdatePersonRelationship(personRelationship);
                    _unitOfWork.Commit();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [HttpPost]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, PersonRelationshipViewModel personRelationship)
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
                    var personRelationshipDb = _personRelationshipRepository.GetSingle(personRelationship.ID);
                    if (personRelationshipDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid.");
                    else
                    {
                        personRelationshipDb.UpdatePersonRelationship(personRelationship);
                        _personRelationshipRepository.Delete(personRelationshipDb);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<PersonRelationshipViewModel>(HttpStatusCode.OK, personRelationship);
                    }
                }

                return response;
            });
        }

        [HttpPost]
        [Route("createall")]
        public HttpResponseMessage CreateAll(HttpRequestMessage request, IEnumerable<PersonRelationshipViewModel> personRelationships)
        {
            List<PersonRelationship> oldPersonRelationships = null;

            oldPersonRelationships = _personRelationshipRepository.FindBy(r => (r.PersonId == 2)).ToList();

            foreach (var oldPersonRelationship in oldPersonRelationships)
            {
                PersonRelationshipViewModel oldPersonRelationshipVM = Mapper.Map<PersonRelationship, PersonRelationshipViewModel>(oldPersonRelationship);
                Delete(request, oldPersonRelationshipVM);
            }

            HttpResponseMessage response = new HttpResponseMessage();
            foreach (var personRelationship in personRelationships)
            {
                response = Create(request, personRelationship);
            }
            return response;
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, PersonRelationshipViewModel personRelationship)
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
                    PersonRelationship newPersonRelationship = new PersonRelationship();
                    newPersonRelationship.UpdatePersonRelationship(personRelationship);

                    _personRelationshipRepository.Add(newPersonRelationship);

                    _unitOfWork.Commit();

                    // Update view model
                    personRelationship = Mapper.Map<PersonRelationship, PersonRelationshipViewModel>(newPersonRelationship);
                    response = request.CreateResponse<PersonRelationshipViewModel>(HttpStatusCode.Created, personRelationship);
                }

                return response;
            });
        }

    }
}