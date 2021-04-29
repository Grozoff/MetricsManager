using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsManagerTests
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;

        public RamMetricsControllerUnitTests()
        {
            controller = new RamMetricsController();
        }
      
        [Fact]
        public void GetAvailableMemorySpace_ReturnsOk()
        {
            //Arrange

            //Act
            var result = controller.GetAvailableMemorySpace();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
