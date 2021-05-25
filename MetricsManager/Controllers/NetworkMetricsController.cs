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
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMapper _mapper;

        public NetworkMetricsController(INetworkMetricsRepository repository, ILogger<NetworkMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public NetworkGetMetricsFromAgentResponse GetMetricsFromAgent([FromRoute] NetworkMetricFromAgentRequests requests)
        {
            _logger.LogInformation(
                   $"Get Network metrics: From Time = {requests.FromTime} " +
                   $"To Time = {requests.ToTime} " +
                   $"from Agent Id = {requests.AgentId}");

            var result = _repository.GetByTimePeriod(requests.FromTime, requests.ToTime, requests.AgentId);

            return new NetworkGetMetricsFromAgentResponse()
            {
                Response = result.Select(_mapper.Map<NetworkMetricResponse>)
            };
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public NetworkGetMetricsFromClusterResponse GetMetricsFromAllCluster([FromRoute] NetworkMetricFromClusterRequests requests)
        {
            _logger.LogInformation($"Get Network metrics: From Time = {requests.FromTime} To Time = {requests.ToTime}");

            var result = _repository.GetByTimePeriod(requests.FromTime, requests.ToTime);

            return new NetworkGetMetricsFromClusterResponse()
            {
                Response = result.Select(_mapper.Map<NetworkMetricResponse>)
            };
        }
    }
}
