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

namespace MetricsManagerTests
{
    public class RamMetricsControllerUnitTests
    {
        private readonly RamMetricsController _controller;
        private readonly Mock<IRamMetricsRepository> _moq;

        public RamMetricsControllerUnitTests()
        {
            _moq = new Mock<IRamMetricsRepository>();
            var logMoq = new Mock<ILogger<RamMetricsController>>();
            _controller = new RamMetricsController(_moq.Object, logMoq.Object);
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _moq.Setup(repository => repository.GetByTimePeriod(fromTime, toTime)).Returns(new List<RamMetric>()).Verifiable();
            RamMetricRequest request = new RamMetricRequest(fromTime, toTime);

            //Act
            var result = _controller.GetMetrics(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
