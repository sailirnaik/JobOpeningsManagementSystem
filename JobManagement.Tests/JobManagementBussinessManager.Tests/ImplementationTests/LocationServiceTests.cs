using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces;
using JobOpeningsManagementMS.Model;
using JobOpeningsManagementMS.Services;
using Moq;
using NUnit.Framework;

namespace JobOpeningsManagementMS.JobManagement.Tests.JobManagementBussinessManager.Tests.ImplementationTests
{
    [TestFixture]
    public class LocationServiceTests
    {
        private Mock<ILocationRepository> _repositoryMock;
        private LocationService _locationService;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<ILocationRepository>();
            _locationService = new LocationService(_repositoryMock.Object);
        }

        [Test]
        public void Create_ShouldAddLocationAndSave()
        {
            var dto = new LocationCreateDTO
            {
                Title = "US Office",
                City = "New York",
                State = "NY",
                Country = "USA",
                Zip = 10001
            };
            _locationService.Create(dto);

            _repositoryMock.Verify(r => r.Add(It.Is<LocationModel>(l =>
                l.Title == dto.Title &&
                l.City == dto.City &&
                l.State == dto.State &&
                l.Country == dto.Country &&
                l.Zip == dto.Zip)), Times.Once);

            _repositoryMock.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void Update_ValidId_ShouldUpdateAndSave()
        {

            var dto = new LocationCreateDTO
            {
                Title = "Updated Office",
                City = "Chicago",
                State = "IL",
                Country = "USA",
                Zip = 60601
            };

            var existingLocation = new LocationModel
            {
                Id = 1,
                Title = "Old Title",
                City = "Old City",
                State = "Old State",
                Country = "Old Country",
                Zip = 12345
            };

            _repositoryMock.Setup(r => r.GetById(1)).Returns(existingLocation);


            _locationService.Update(1, dto);

            Assert.AreEqual(dto.Title, existingLocation.Title);
            Assert.AreEqual(dto.City, existingLocation.City);
            Assert.AreEqual(dto.State, existingLocation.State);
            Assert.AreEqual(dto.Country, existingLocation.Country);
            Assert.AreEqual(dto.Zip, existingLocation.Zip);

            _repositoryMock.Verify(r => r.Update(existingLocation), Times.Once);
            _repositoryMock.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void Update_InvalidId_ShouldThrowException()
        {
            _repositoryMock.Setup(r => r.GetById(It.IsAny<int>())).Returns((LocationModel)null!);

            var dto = new LocationCreateDTO
            {
                Title = "Invalid",
                City = "X",
                State = "Y",
                Country = "Z",
                Zip = 111
            };

            var ex = Assert.Throws<Exception>(() => _locationService.Update(1, dto));
            Assert.That(ex.Message, Is.EqualTo("Location not found"));
        }

        [Test]
        public void GetAll_ShouldReturnAllLocations()
        {
            var locations = new List<LocationModel>
            {
                new LocationModel { Id = 1, Title = "Loc1", City = "C1", State = "S1", Country = "US", Zip = 123 },
                new LocationModel { Id = 2, Title = "Loc2", City = "C2", State = "S2", Country = "IN", Zip = 456 }
            };

            _repositoryMock.Setup(r => r.GetAll()).Returns(locations);

            var result = _locationService.GetAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Loc1", result[0].Title);
            Assert.AreEqual("Loc2", result[1].Title);
        }
    }
}
