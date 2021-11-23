using AutoMapper;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MetricsAgent.Controllers
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

        /// <summary>
        /// Получает метрики Network на заданном диапазоне времени
        /// </summary>
        /// <param name="request">Диапазон времени</param>
        /// <returns>Список метрик</returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">Eсли передали не правильные параметры</response>
        [HttpGet("from/{FromTime}/to/{ToTime}")]
        public NetworkMetricsByTimePeriodResponse GetMetrics([FromRoute] NetworkMetricRequest request)
        {
            _logger.LogInformation($"Get Network metrics: From Time = {request.FromTime} To Time = {request.ToTime}");

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            return new NetworkMetricsByTimePeriodResponse()
            {
                Response = result.Select(_mapper.Map<NetworkMetricDto>)
            };
        }
    }
}
