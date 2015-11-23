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
    [RoutePrefix("api/data")]
    public class DataController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Ethnicity> _ethnicityRepository;
        private readonly IEntityBaseRepository<Diagnosis> _diagnosisRepository;
        private readonly IEntityBaseRepository<DiagnosisSubType> _diagnosisSubTypeRepository;
        private readonly IEntityBaseRepository<RelationshipType> _relationshipTypeRepository;

        public DataController(IEntityBaseRepository<Ethnicity> ethnicityRepository, 
                                IEntityBaseRepository<Diagnosis> diagnosisRepository,
                                IEntityBaseRepository<DiagnosisSubType> diagnosisSubTypeRepository,
                                IEntityBaseRepository<RelationshipType> relationshipTypeRepository,
                                IEntityBaseRepository<Error> _errorsRepository, 
                                IUnitOfWork _unitOfWork)
                                : base(_errorsRepository, _unitOfWork)
        {
            _ethnicityRepository = ethnicityRepository;
            _diagnosisRepository = diagnosisRepository;
            _diagnosisSubTypeRepository = diagnosisSubTypeRepository;
        }

        [HttpGet]
        [Route("relationhipType")]
        public HttpResponseMessage ListRelationshipType(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<RelationshipType> relationshipType = null;
                relationshipType = _relationshipTypeRepository.GetAll()
                    .OrderBy(c => c.ID)
                    .ToList();

                IEnumerable<RelationshipTypeViewModel> relationshipTypeVM = Mapper.Map<IEnumerable<RelationshipType>,
                    IEnumerable<RelationshipTypeViewModel>>(relationshipType);

                response = request.CreateResponse<IEnumerable<RelationshipTypeViewModel>>(HttpStatusCode.OK, relationshipTypeVM);

                return response;
            });
        }

        [HttpGet]
        [Route("ethnicity")]
        public HttpResponseMessage ListEthnicity(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Ethnicity> ethnicity = null;
                ethnicity = _ethnicityRepository.GetAll()
                    .OrderBy(c => c.ID)
                    .ToList();

                IEnumerable<EthnicityViewModel> ethnicityVM = Mapper.Map<IEnumerable<Ethnicity>, 
                    IEnumerable<EthnicityViewModel>>(ethnicity);

                response = request.CreateResponse<IEnumerable<EthnicityViewModel>>(HttpStatusCode.OK, ethnicityVM);

                return response;
            });
        }

        [HttpGet]
        [Route("diagnosis")]
        public HttpResponseMessage ListDiagnosis(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Diagnosis> diagnosis = null;
                diagnosis = _diagnosisRepository.GetAll()
                    .OrderBy(c => c.ID)
                    .ToList();

                IEnumerable<DiagnosisViewModel> diagnosisVM = Mapper.Map<IEnumerable<Diagnosis>,
                    IEnumerable<DiagnosisViewModel>>(diagnosis);

                response = request.CreateResponse<IEnumerable<DiagnosisViewModel>>(HttpStatusCode.OK, diagnosisVM);

                return response;
            });
        }

        [HttpGet]
        [Route("diagnosisSubType/{diagnosisId:int=0}")]
        public HttpResponseMessage ListDiagnosisSubType(HttpRequestMessage request, int diagnosisId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<DiagnosisSubType> diagnosisSubType = null;
                diagnosisSubType = _diagnosisSubTypeRepository.FindBy(c => c.DiagnosisId.Equals(diagnosisId))
                    .OrderBy(c => c.ID)
                    .ToList();

                IEnumerable<DiagnosisSubTypeViewModel> diagnosisSubTypeVM = Mapper.Map<IEnumerable<DiagnosisSubType>,
                    IEnumerable<DiagnosisSubTypeViewModel>>(diagnosisSubType);

                response = request.CreateResponse<IEnumerable<DiagnosisSubTypeViewModel>>(HttpStatusCode.OK, diagnosisSubTypeVM);

                return response;
            });
        }

    }
}