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
    public class DotNetMetricsControllerUnitTests
    {
        private readonly DotNetMetricsController _controller;
        private readonly Mock<IDotNetMetricsRepository> _moq;

        public DotNetMetricsControllerUnitTests()
        {
            _moq = new Mock<IDotNetMetricsRepository>();
            var logMoq = new Mock<ILogger<DotNetMetricsController>>();
            _controller = new DotNetMetricsController(_moq.Object, logMoq.Object);
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _moq.Setup(repository => repository.GetByTimePeriod(fromTime, toTime)).Returns(new List<DotNetMetric>()).Verifiable();
            DotNetMetricRequest request = new DotNetMetricRequest(fromTime, toTime);

            //Act
            var result = _controller.GetMetrics(request);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
