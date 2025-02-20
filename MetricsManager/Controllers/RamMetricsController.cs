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

        /// <summary>
        /// Получает метрики RAM на заданном диапазоне времени по Id агента
        /// </summary>
        /// <param name="requests">Id агента и диапазон времени</param>
        /// <returns>Список метрик с одного агента</returns>
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
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

        /// <summary>
        /// Получает метрики RAM на заданном диапазоне времени со всех агентов
        /// </summary>
        /// <param name="requests">Диапазон времени</param>
        /// <returns>Список метрик со всех агентов</returns>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
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
