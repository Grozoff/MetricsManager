using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsManagerTests
{
    public class HddMetricsControllerUnitTests
    {
        private HddMetricsController controller;

        public HddMetricsControllerUnitTests()
        {
            controller = new HddMetricsController();
        }

        [Fact]
        public void GetLeftSpace_ReturnsOk()
        {
            //Arrange

            //Act
            var result = controller.GetLeftSpace();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
