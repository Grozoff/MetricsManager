using AutoMapper;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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
        public IActionResult GetMetrics([FromRoute] DotNetMetricRequest request)
        {
            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            var response = new DotNetMetricsByTimePeriodResponse()
            {
                Response = new List<DotNetMetricDto>()
            };
            foreach (var metrics in result)
            {
                response.Response.Add(_mapper.Map<DotNetMetricDto>(metrics));
            }

            _logger.LogInformation($"Get CPU metrics: From Time = {request.FromTime} To Time = {request.ToTime}");

            return Ok(response);
        }
    }
}
