﻿using AutoMapper;
using MetricsManager.Controllers.Requests;
using MetricsManager.Controllers.Responses;
using MetricsManager.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly IHddMetricsRepository _repository;
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMapper _mapper;

        public HddMetricsController(IHddMetricsRepository repository, ILogger<HddMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает метрики HDD на заданном диапазоне времени по Id агента
        /// </summary>
        /// <param name="requests">Id агента и диапазон времени</param>
        /// <returns>Список метрик с одного агента</returns>
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public HddGetMetricsFromAgentResponse GetMetricsFromAgent([FromRoute] HddMetricFromAgentRequests requests)
        {
            _logger.LogInformation(
                   $"Get Hdd metrics: From Time = {requests.FromTime} " +
                   $"To Time = {requests.ToTime} " +
                   $"from Agent Id = {requests.AgentId}");

            var result = _repository.GetByTimePeriod(requests.FromTime, requests.ToTime, requests.AgentId);

            return new HddGetMetricsFromAgentResponse()
            {
                Response = result.Select(_mapper.Map<HddMetricResponse>)
            };
        }

        /// <summary>
        /// Получает метрики HDD на заданном диапазоне времени со всех агентов
        /// </summary>
        /// <param name="requests">Диапазон времени</param>
        /// <returns>Список метрик со всех агентов</returns>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public HddGetMetricsFromClusterResponse GetMetricsFromAllCluster([FromRoute] HddMetricFromClusterRequests requests)
        {
            _logger.LogInformation($"Get Hdd metrics: From Time = {requests.FromTime} To Time = {requests.ToTime}");

            var result = _repository.GetByTimePeriod(requests.FromTime, requests.ToTime);

            return new HddGetMetricsFromClusterResponse()
            {
                Response = result.Select(_mapper.Map<HddMetricResponse>)
            };
        }
    }
}
