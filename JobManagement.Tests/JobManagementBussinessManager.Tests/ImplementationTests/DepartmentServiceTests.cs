using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Implementation;
using JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces;
using JobOpeningsManagementMS.Model;
using Moq;
using NUnit.Framework;

namespace JobOpeningsManagementMS.JobManagement.Tests.JobManagementBussinessManager.Tests.ImplementationTests
{
    [TestFixture]
    public class DepartmentServiceTests
    {
        private Mock<IDepartmentRepository> _mockRepo;
        private DepartmentService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IDepartmentRepository>();
            _service = new DepartmentService(_mockRepo.Object);
        }

        [Test]
        public void Create_ShouldAddDepartmentAndSave()
        {
            var dto = new DepartmentCreateDTO { Title = "IT" };

            _service.Create(dto);

            _mockRepo.Verify(r => r.Add(It.Is<DepartmentModel>(d => d.Title == "IT")), Times.Once);
            _mockRepo.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void Update_ValidId_ShouldUpdateAndSave()
        {
            var dto = new DepartmentCreateDTO { Title = "Updated" };
            var department = new DepartmentModel { Id = 1, Title = "Old" };

            _mockRepo.Setup(r => r.GetById(1)).Returns(department);

            _service.Update(1, dto);

            Assert.AreEqual("Updated", department.Title);
            _mockRepo.Verify(r => r.Update(department), Times.Once);
            _mockRepo.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void Update_InvalidId_ShouldThrowException()
        {
            _mockRepo.Setup(r => r.GetById(1)).Returns((DepartmentModel)null!);

            var dto = new DepartmentCreateDTO { Title = "Any" };

            var ex = Assert.Throws<Exception>(() => _service.Update(1, dto));
            Assert.That(ex.Message, Is.EqualTo("Department not found"));
        }

        [Test]
        public void GetAll_ShouldReturnMappedDTOs()
        {
            var departments = new List<DepartmentModel>
            {
                new DepartmentModel { Id = 1, Title = "HR" },
                new DepartmentModel { Id = 2, Title = "Finance" }
            };

            _mockRepo.Setup(r => r.GetAll()).Returns(departments);

            var result = _service.GetAll();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("HR", result[0].Title);
            Assert.AreEqual("Finance", result[1].Title);
        }
    }
}
