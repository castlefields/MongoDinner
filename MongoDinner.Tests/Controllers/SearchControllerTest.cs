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
    public class SearchControllerTest {

        SearchController CreateSearchController() {
            var testData = FakeDinnerData.CreateTestDinners();
            var repository = new FakeDinnerRepository(testData);

            return new SearchController(repository);
        }

        [Test]
        public void SearchByLocationAction_Should_Return_Json()
        {

            // Arrange
            var controller = CreateSearchController();

            // Act
            var result = controller.SearchByLocation(99, -99);

            // Assert
            //Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [Test]
        public void SearchByLocationAction_Should_Return_JsonDinners()
        {

            // Arrange
            var controller = CreateSearchController();

            // Act
            var result = (JsonResult)controller.SearchByLocation(99, -99);

            // Assert
            //Assert.IsInstanceOfType(result.Data, typeof(List<JsonDinner>));
            var dinners = (List<JsonDinner>)result.Data;
            Assert.AreEqual(101, dinners.Count);
        }
    }
}
