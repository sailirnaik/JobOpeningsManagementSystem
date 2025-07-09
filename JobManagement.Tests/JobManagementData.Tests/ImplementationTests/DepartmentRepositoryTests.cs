using JobOpeningsManagementMS.Data;
using JobOpeningsManagementMS.JobManagementAPI.Data.Implementation;
using JobOpeningsManagementMS.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace JobOpeningsManagementMS.JobManagement.Tests.JobManagementData.Tests.ImplementationTests
{
    [TestFixture]
    public class DepartmentRepositoryTests
    {
        private AppDbContext _context;
        private DepartmentRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DepartmentTestDb")
                .Options;

            _context = new AppDbContext(options);
            _repository = new DepartmentRepository(_context);

            _context.Departments.AddRange(new List<DepartmentModel>
            {
                new DepartmentModel { Id = 1, Title = "HR" },
                new DepartmentModel { Id = 2, Title = "Engineering" }
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
        public void GetAll_ShouldReturnAllDepartments()
        {
            var result = _repository.GetAll();
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void GetById_ValidId_ShouldReturnDepartment()
        {
            var result = _repository.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("HR", result.Title);
        }

        [Test]
        public void GetById_InvalidId_ShouldReturnNull()
        {
            var result = _repository.GetById(999);
            Assert.IsNull(result);
        }

        [Test]
        public void Add_ShouldAddDepartment()
        {
            var newDept = new DepartmentModel { Title = "Finance" };
            _repository.Add(newDept);
            _repository.Save();

            var result = _context.Departments.FirstOrDefault(d => d.Title == "Finance");
            Assert.IsNotNull(result);
        }

        [Test]
        public void Update_ShouldModifyDepartment()
        {
            var existing = _context.Departments.First();
            existing.Title = "Updated HR";

            _repository.Update(existing);
            _repository.Save();

            var updated = _context.Departments.First(d => d.Id == existing.Id);
            Assert.AreEqual("Updated HR", updated.Title);
        }
    }
}
