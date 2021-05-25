using AutoMapper;
using MetricsManager.Controllers.Requests;
using MetricsManager.Controllers.Responses;
using MetricsManager.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly IRamMetricsRepository _repository;
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IMapper _mapper;

        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("agent/{agentId}")]
        public RamGetMetricsFromAgentResponse GetMetricsFromAgent([FromRoute] RamMetricFromAgentRequests requests)
        {
            _logger.LogInformation(
                   $"Get Ram metrics: From Time = {requests.FromTime} " +
                   $"To Time = {requests.ToTime} " +
                   $"from Agent Id = {requests.AgentId}");

            var result = _repository.GetByTimePeriod(requests.FromTime, requests.ToTime, requests.AgentId);

            return new RamGetMetricsFromAgentResponse()
            {
                Response = result.Select(_mapper.Map<RamMetricResponse>)
            };
        }

        [HttpGet("cluster")]
        public RamGetMetricsFromClusterResponse GetMetricsFromAllCluster([FromRoute] RamMetricFromClusterRequests requests)
        {
            _logger.LogInformation($"Get Ram metrics: From Time = {requests.FromTime} To Time = {requests.ToTime}");

            var result = _repository.GetByTimePeriod(requests.FromTime, requests.ToTime);

            return new RamGetMetricsFromClusterResponse()
            {
                Response = result.Select(_mapper.Map<RamMetricResponse>)
            };
        }
    }
}
