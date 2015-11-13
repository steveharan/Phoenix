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
    [RoutePrefix("api/ethnicity")]
    public class EthnicityController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Ethnicity> _ethnicityRepository;

        public EthnicityController(IEntityBaseRepository<Ethnicity> ethnicityRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _ethnicityRepository = ethnicityRepository;
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage List(HttpRequestMessage request)
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
    }
}