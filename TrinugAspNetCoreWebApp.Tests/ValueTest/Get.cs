using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using TrinugAspNetCoreWebApp.Controllers;
using Xunit;

namespace ValueTest
{
    public class Get
    {
        private ValueController Arrange()
        {
            return new ValueController();
        }

        [Fact]
        public void Returns_Correct_Value()
        {
            //Arrange
            var controller = Arrange();

            //Act
            var result = controller.Get(2);

            //Assert
            var okObjectResult = result.Should().BeAssignableTo<OkObjectResult>().Subject;
            (okObjectResult.Value).Should().Be("value 2");
        }

        [Fact]
        public void MissingId_Returns_StatusCode_NotFound()
        {
            //Arrange
            var controller = Arrange();

            //Act
            var result = controller.Get(101);

            //Assert
            result.Should().BeAssignableTo<NotFoundResult>();
        }
    }
}
