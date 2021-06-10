using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.DAL.Models;
using AutoMapper;
using MetricsAgent.Controllers.Responses;

namespace MetricsManagerTests
{
    public class HddMetricsControllerUnitTests
    {
        private readonly HddMetricsController _controller;
        private readonly Mock<IHddMetricsRepository> _moq;
        private readonly IMapper _mapper;

        public HddMetricsControllerUnitTests()
        {
            _moq = new Mock<IHddMetricsRepository>();
            var logMoq = new Mock<ILogger<HddMetricsController>>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetric, HddMetricDto>());
            _mapper = config.CreateMapper();
            _controller = new HddMetricsController(_moq.Object, logMoq.Object, _mapper);
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _moq.Setup(repository => repository.GetByTimePeriod(fromTime, toTime)).Returns(new List<HddMetric>()).Verifiable();
            HddMetricRequest request = new HddMetricRequest(fromTime, toTime);

            //Act
            var result = _controller.GetMetrics(request);

            // Assert
            _ = Assert.IsAssignableFrom<HddMetricsByTimePeriodResponse>(result);
        }
    }
}
