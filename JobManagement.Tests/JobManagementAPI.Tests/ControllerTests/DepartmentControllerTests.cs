using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.API.Controllers;
using JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace JobOpeningsManagementMS.JobManagement.Tests.JobManagementAPI.Tests.ControllerTests
{
    [TestFixture]
    public class DepartmentControllerTests
    {
        private Mock<IDepartmentService> _mockService;
        private DepartmentController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IDepartmentService>();
            _controller = new DepartmentController(_mockService.Object);
        }

        [Test]
        public void Create_ShouldReturn201Status()
        {
            var dto = new DepartmentCreateDTO { Title = "HR" };

            var result = _controller.Create(dto) as StatusCodeResult;

            _mockService.Verify(s => s.Create(dto), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
        }

        [Test]
        public void Update_ShouldReturn200Status()
        {
            var dto = new DepartmentCreateDTO { Title = "IT" };

            var result = _controller.Update(1, dto) as OkResult;

            _mockService.Verify(s => s.Update(1, dto), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void GetAll_ShouldReturnListOfDepartments()
        {
            var departments = new List<DepartmentDTO>
            {
                new DepartmentDTO { Id = 1, Title = "Finance" },
                new DepartmentDTO { Id = 2, Title = "Engineering" }
            };

            _mockService.Setup(s => s.GetAll()).Returns(departments);

            var result = _controller.GetAll() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<List<DepartmentDTO>>(result.Value);

            var actualList = result.Value as List<DepartmentDTO>;
            Assert.AreEqual(2, actualList.Count);
        }
    }
}
