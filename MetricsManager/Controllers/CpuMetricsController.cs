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
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMapper _mapper;

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает метрики CPU на заданном диапазоне времени по Id агента
        /// </summary>
        /// <param name="requests">Id агента и диапазон времени</param>
        /// <returns>Список метрик с одного агента</returns>
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public CpuGetMetricsFromAgentResponse GetMetricsFromAgent([FromRoute] CpuMetricFromAgentRequests requests)
        {
            _logger.LogInformation(
                $"Get CPU metrics: From Time = {requests.FromTime} " +
                $"To Time = {requests.ToTime} " +
                $"from Agent Id = {requests.AgentId}");

            var result = _repository.GetByTimePeriod(requests.FromTime, requests.ToTime , requests.AgentId);

            return new CpuGetMetricsFromAgentResponse()
            {
                Response = result.Select(_mapper.Map<CpuMetricResponse>)
            };
        }

        /// <summary>
        /// Получает метрики CPU на заданном диапазоне времени со всех агентов
        /// </summary>
        /// <param name="requests">Диапазон времени</param>
        /// <returns>Список метрик со всех агентов</returns>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public CpuGetMetricsFromClusterResponse GetMetricsFromAllCluster([FromRoute] CpuMetricFromClusterRequests requests)
        {
            _logger.LogInformation($"Get CPU metrics: From Time = {requests.FromTime} To Time = {requests.ToTime}");

            var result = _repository.GetByTimePeriod(requests.FromTime, requests.ToTime);

            return new CpuGetMetricsFromClusterResponse()
            {
                Response = result.Select(_mapper.Map<CpuMetricResponse>)
            };
        }
    }
}
