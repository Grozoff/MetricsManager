using AutoMapper;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace MetricsAgent.Controllers
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
        /// Получает метрики HDD на заданном диапазоне времени
        /// </summary>
        /// <param name="request">Диапазон времени</param>
        /// <returns>Список метрик</returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">Eсли передали не правильные параметры</response>
        [HttpGet("from/{FromTime}/to/{ToTime}")]
        public HddMetricsByTimePeriodResponse GetMetrics([FromRoute] HddMetricRequest request)
        {
            _logger.LogInformation($"Get Hdd metrics: From Time = {request.FromTime} To Time = {request.ToTime}");

            var result = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            return new HddMetricsByTimePeriodResponse()
            {
                Response = result.Select(_mapper.Map<HddMetricDto>)
            };
        }
    }
}
