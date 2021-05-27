using AutoMapper;
using MetricsManager;
using MetricsManager.Controllers;
using MetricsManager.Controllers.Requests;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private readonly AgentsController _controller;
        private readonly Mock<IAgentRepository> _moq;

        public AgentsControllerUnitTests()
        {
            _moq = new Mock<IAgentRepository>();
            var logMoq = new Mock<ILogger<AgentsController>>();
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            _controller = new AgentsController(_moq.Object, logMoq.Object, mapper);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            _moq.Setup(repo => repo.GetAll()).Returns(new List<AgentInfo>()).Verifiable();
            //Act
            var result = _controller.RegisterAgent(new AgentRequest());
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            //Arrange
            var agentId = 1;

            //Act
            var result = _controller.EnableAgentById(agentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            //Arrange
            var agentId = 1;

            //Act
            var result = _controller.DisableAgentById(agentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void GetRegisteredAgents_ReturnsEmptyEnumerable()
        {
            //Arrange
            _moq.Setup(repo => repo.GetAll()).Returns(new List<AgentInfo>()).Verifiable();

            //Act
            var result = _controller.GetAllRegisteredAgents();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
