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
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly IRamMetricsRepository _repository;
        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailableMemorySpace([FromRoute] HddMetricRequest request)
        {
            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            var response = new RamMetricsByTimePeriodResponse()
            {
                Response = new List<string>()
            };
            foreach (var metrics in result)
            {
                response.Response.Add($"Date: {DateTimeOffset.FromUnixTimeSeconds(metrics.Time)}    Metric: {metrics.Value}");
            }

            _logger.LogInformation($"Get CPU metrics: From Time = {request.FromTime} To Time = {request.ToTime}");

            if (result == null)
            {
                return NotFound();
            }
            return Ok(response.Response.ToList());
        }
    }
}
