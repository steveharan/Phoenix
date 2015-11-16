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

        public DataController(IEntityBaseRepository<Ethnicity> ethnicityRepository, IEntityBaseRepository<Diagnosis> diagnosisRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _ethnicityRepository = ethnicityRepository;
            _diagnosisRepository = diagnosisRepository;
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

    }
}