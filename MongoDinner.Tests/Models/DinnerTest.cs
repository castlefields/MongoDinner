using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;


using MongoDinner.Models;

namespace MongoDinner.Tests.Models {

    [TestFixture]
    public class DinnerTest {

        [Test]
        public void Dinner_Should_Not_Be_Valid_When_Some_Properties_Incorrect() {

            //Arrange
            Dinner dinner = new Dinner() {
                Title = "Test title",
                Country = "USA",
                ContactPhone = "BOGUS"
            };

            // Act
            bool isValid = dinner.IsValid;

            //Assert
            Assert.IsFalse(isValid);
        }

        [Test]
        public void Dinner_Should_Be_Valid_When_All_Properties_Correct() {

            Location location = new Location
            {
                Latitude = 93,
                Longitude = -92
            };
            //Arrange
            Dinner dinner = new Dinner {
                Title = "Test title",
                Description = "Some description",
                EventDate = DateTime.Now,
                HostedBy = "ScottGu",
                Address = "One Microsoft Way",
                Country = "USA",
                ContactPhone = "425-703-8072",
                Location = location
            };

            // Act
            bool isValid = dinner.IsValid;

            //Assert
            Assert.IsTrue(isValid);
        }
    }
}
