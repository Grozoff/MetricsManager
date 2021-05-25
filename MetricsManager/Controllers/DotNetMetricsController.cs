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

        public ILogger<DotNetMetricsController> Logger => _logger;

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public DotNetGetMetricsFromAgentResponse GetMetricsFromAgent([FromRoute] DotNetMetricFromAgentRequests requests)
        {
            _logger.LogInformation(
                   $"Get DotNet metrics: From Time = {requests.FromTime} " +
                   $"To Time = {requests.ToTime} " +
                   $"from Agent Id = {requests.AgentId}");

            var result = _repository.GetByTimePeriod(requests.FromTime, requests.ToTime, requests.AgentId);

            return new DotNetGetMetricsFromAgentResponse()
            {
                Response = result.Select(_mapper.Map<DotNetMetricResponse>)
            };          
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public DotNetGetMetricsFromClusterResponse GetMetricsFromAllCluster([FromRoute] DotNetMetricFromClusterRequests requests)
        {
            _logger.LogInformation($"Get DotNet metrics: From Time = {requests.FromTime} To Time = {requests.ToTime}");

            var result = _repository.GetByTimePeriod(requests.FromTime, requests.ToTime);

            return new DotNetGetMetricsFromClusterResponse()
            {
                Response = result.Select(_mapper.Map<DotNetMetricResponse>)
            };
        }
    }
}
