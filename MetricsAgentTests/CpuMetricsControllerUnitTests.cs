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
using MetricsManager.Controllers.Responses;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerUnitTests
    {
        private readonly CpuMetricsController _controller;
        private readonly Mock<ICpuMetricsRepository> _moq;
        private readonly IMapper _mapper;

        public CpuMetricsControllerUnitTests()
        {
            _moq = new Mock<ICpuMetricsRepository>();
            var logMoq = new Mock<ILogger<CpuMetricsController>>();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetric, CpuMetricDto>());
            _mapper = config.CreateMapper();
            _controller = new CpuMetricsController(_moq.Object, logMoq.Object, _mapper);
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            _moq.Setup(repository => repository.GetByTimePeriod(fromTime, toTime)).Returns(new List<CpuMetric>()).Verifiable();
            CpuMetricRequest request = new CpuMetricRequest(fromTime, toTime);

            //Act
            var result = _controller.GetMetrics(request);

            // Assert
            _ = Assert.IsAssignableFrom<CpuMetricsByTimePeriodResponse>(result);           
        }
    }
}
