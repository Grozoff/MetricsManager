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
    public class NetworkMetricsControllerUnitTests
    {
        private readonly NetworkMetricsController _controller;
        private readonly Mock<INetworkMetricsRepository> _moq;
        private readonly IMapper _mapper;

        public NetworkMetricsControllerUnitTests()
        {
            _moq = new Mock<INetworkMetricsRepository>();
            var logMoq = new Mock<ILogger<NetworkMetricsController>>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetric, NetworkMetricDto>());
            _mapper = config.CreateMapper();
            _controller = new NetworkMetricsController(_moq.Object, logMoq.Object, _mapper);
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _moq.Setup(repository => repository.GetByTimePeriod(fromTime, toTime)).Returns(new List<NetworkMetric>()).Verifiable();
            NetworkMetricRequest request = new NetworkMetricRequest(fromTime, toTime);

            //Act
            var result = _controller.GetMetrics(request);

            // Assert
            _ = Assert.IsAssignableFrom<NetworkMetricsByTimePeriodResponse>(result);
        }
    }
}
