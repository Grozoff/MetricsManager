using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL.Models;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.Controllers.Requests;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] CpuMetricRequest request)
        {
            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            var response = new CpuMetricsByTimePeriodResponse()
            {
                Response = new List<CpuMetricDto>()
            };

            foreach (var metrics in result)
            {
                response.Response.Add(new CpuMetricDto
                {
                    Time = DateTimeOffset.FromUnixTimeSeconds(metrics.Time),
                    Value = metrics.Value
                });
            }

            _logger.LogInformation($"Get CPU metrics: From Time = {request.FromTime} To Time = {request.ToTime}");
           
            return Ok(response);
        }
    }
}
