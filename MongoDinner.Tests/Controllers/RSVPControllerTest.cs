using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;


using MongoDinner.Controllers;
using System.Web.Mvc;
using MongoDinner.Models;
using MongoDinner.Tests.Fakes;
using Moq;
using MongoDinner.Helpers;
using System.Web.Routing;

namespace MongoDinner.Tests.Controllers {
 
    [TestFixture]
    public class RSVPControllerTest {

        RSVPController CreateRSVPController() {
            var testData = FakeDinnerData.CreateTestDinners();
            var repository = new FakeDinnerRepository(testData);

            return new RSVPController(repository);
        }

        RSVPController CreateRSVPControllerAs(string userName)
        {

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = CreateRSVPController();
            controller.ControllerContext = mock.Object;

            return controller;
        }

        [Test]
        public void RegisterAction_Should_Return_Content()
        {
            // Arrange
            var controller = CreateRSVPControllerAs("scottha");

            // Act
            //var result = controller.Register(1);

            // Assert
            //Assert.IsInstanceOfType(result, typeof(ContentResult));
        }
    }
}
