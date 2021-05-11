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
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(INetworkMetricsRepository repository, ILogger<NetworkMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] NetworkMetricRequest request)
        {
            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);
            var response = new NetworkMetricsByTimePeriodResponse()
            {
                Response = new List<NetworkMetricDto>()
            };
            foreach (var metrics in result)
            {
                response.Response.Add(new NetworkMetricDto
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
