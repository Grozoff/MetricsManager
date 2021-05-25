using AutoMapper;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly IDotNetMetricsRepository _repository;
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IMapper _mapper;

        public DotNetMetricsController(IDotNetMetricsRepository repository, ILogger<DotNetMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public DotNetMetricsByTimePeriodResponse GetMetrics([FromRoute] DotNetMetricRequest request)
        {
            _logger.LogInformation($"Get DotNet metrics: From Time = {request.FromTime} To Time = {request.ToTime}");

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            return new DotNetMetricsByTimePeriodResponse()
            {
                Response = result.Select(_mapper.Map<DotNetMetricDto>)
            };

        }
    }
}
