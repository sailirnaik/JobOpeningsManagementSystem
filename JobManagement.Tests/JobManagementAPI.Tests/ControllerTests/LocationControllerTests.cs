using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.API.Controllers;
using JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace JobOpeningsManagementMS.JobManagement.Tests.JobManagementAPI.Tests.ControllerTests
{
     [TestFixture]
        public class LocationControllerTests
        {
            private Mock<ILocationService> _locationServiceMock;
            private LocationController _controller;

            [SetUp]
            public void SetUp()
            {
                _locationServiceMock = new Mock<ILocationService>();
                _controller = new LocationController(_locationServiceMock.Object);
            }

            [Test]
            public void Create_ShouldReturn201StatusCode()
            {
                var dto = new LocationCreateDTO
                {
                    Title = "New York Office",
                    City = "New York",
                    State = "NY",
                    Country = "USA",
                    Zip = 10001
                };

                var result = _controller.Create(dto) as StatusCodeResult;

                _locationServiceMock.Verify(s => s.Create(dto), Times.Once);
                Assert.IsNotNull(result);
                Assert.AreEqual(201, result.StatusCode);
            }

            [Test]
            public void Update_ShouldReturn200StatusCode()
            {
                var dto = new LocationCreateDTO
                {
                    Title = "Updated Office",
                    City = "Chicago",
                    State = "IL",
                    Country = "USA",
                    Zip = 60601
                };

                var result = _controller.Update(1, dto) as OkResult;

                _locationServiceMock.Verify(s => s.Update(1, dto), Times.Once);
                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
            }

            [Test]
            public void GetAll_ShouldReturnListOfLocations()
            {
                var expectedList = new List<LocationDTO>
            {
                new LocationDTO { Id = 1, Title = "Office A", City = "City A", State = "State A", Country = "Country A", Zip = 12345 },
                new LocationDTO { Id = 2, Title = "Office B", City = "City B", State = "State B", Country = "Country B", Zip = 54321 }
            };

                _locationServiceMock.Setup(s => s.GetAll()).Returns(expectedList);

                var result = _controller.GetAll() as OkObjectResult;

                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);
                Assert.IsInstanceOf<List<LocationDTO>>(result.Value);

                var actualList = result.Value as List<LocationDTO>;
                Assert.AreEqual(expectedList.Count, actualList.Count);
            }
        }
    
}
