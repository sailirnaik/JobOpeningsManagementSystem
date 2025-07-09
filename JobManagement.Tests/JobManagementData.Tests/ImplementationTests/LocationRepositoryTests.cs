using JobOpeningsManagementMS.Data.Implementation;
using JobOpeningsManagementMS.Data;
using JobOpeningsManagementMS.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;


namespace JobOpeningsManagementMS.JobManagement.Tests.JobManagementData.Tests.ImplementationTests
{
    [TestFixture]
    public class LocationRepositoryTests
    {
        private AppDbContext _context;
        private LocationRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _repository = new LocationRepository(_context);

            _context.Locations.AddRange(new List<LocationModel>
            {
                new LocationModel { Id = 1, Title = "Head Office", City = "Bangalore", State = "Karnataka", Country = "India", Zip = 560001 },
                new LocationModel { Id = 2, Title = "US Office", City = "New York", State = "NY", Country = "USA", Zip = 10001 }
            });
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void GetAll_ShouldReturnAllLocations()
        {
            var result = _repository.GetAll();
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void GetById_ValidId_ReturnsLocation()
        {
            var result = _repository.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Head Office", result.Title);
        }

        [Test]
        public void GetById_InvalidId_ReturnsNull()
        {
            var result = _repository.GetById(999);
            Assert.IsNull(result);
        }

        [Test]
        public void Add_ShouldAddLocationToDatabase()
        {
            var newLocation = new LocationModel
            {
                Title = "New Branch",
                City = "Pune",
                State = "MH",
                Country = "India",
                Zip = 411001
            };

            _repository.Add(newLocation);
            _repository.Save();

            var result = _context.Locations.FirstOrDefault(l => l.Title == "New Branch");
            Assert.IsNotNull(result);
            Assert.AreEqual("Pune", result.City);
        }

        [Test]
        public void Update_ShouldModifyLocation()
        {
            var existing = _context.Locations.First();
            existing.City = "Updated City";

            _repository.Update(existing);
            _repository.Save();

            var updated = _context.Locations.First(l => l.Id == existing.Id);
            Assert.AreEqual("Updated City", updated.City);
        }
    }
}
