using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using JobOpeningsManagementMS.Controllers;
using JobOpeningsManagementMS.Services;
using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;

namespace JobOpeningsManagementMS.JobManagement.Tests.JobManagementAPI.Tests.ControllerTests
{
    [TestFixture]
    public class JobControllerTests
    {
        private Mock<IJobService> _mockJobService;
        private JobController _controller;

        [SetUp]
        public void Setup()
        {
            _mockJobService = new Mock<IJobService>();
            _controller = new JobController(_mockJobService.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "https";
            httpContext.Request.Host = new HostString("localhost");
            _controller.ControllerContext = new ControllerContext() { HttpContext = httpContext };
        }

        [Test]
        public void CreateJob_ReturnsCreated()
        {
            var dto = new CreateJobRequestDTO();
            _mockJobService.Setup(s => s.CreateJob(dto)).Returns(1);
            var result = _controller.Create(dto).Result as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.That(result.Value.ToString(), Does.Contain("/api/v1/jobs/1"));
        }

        [Test]
        public void UpdateJob_Valid_ReturnsOk()
        {
            var dto = new CreateJobRequestDTO();
            _mockJobService.Setup(s => s.UpdateJob(1, dto)).Returns(true);
            var result = _controller.Update(1, dto);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void UpdateJob_Invalid_ReturnsNotFound()
        {
            var dto = new CreateJobRequestDTO();
            _mockJobService.Setup(s => s.UpdateJob(99, dto)).Returns(false);
            var result = _controller.Update(99, dto);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void UpdateJob_ThrowsException_ReturnsNotFoundWithMessage()
        {
            var dto = new CreateJobRequestDTO();
            _mockJobService.Setup(s => s.UpdateJob(It.IsAny<int>(), It.IsAny<CreateJobRequestDTO>()))
                .Throws(new Exception("error"));
            var result = _controller.Update(1, dto) as NotFoundObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("error", result.Value);
        }

        [Test]
        public void GetListOfJobs_ReturnsOk()
        {
            var req = new ListOfJobsRequestDTO();
            var expected = new ListOfJobsResponseDTO
            {
                Total = 1,
                Data = new List<JobResponseDTO>
                {
                    new JobResponseDTO
                    {
                        Id = 1,
                        Code = "JOB-01",
                        Title = "Software Developer",
                        Description = "Job description here...",
                        Location = "US Head Office",
                        Department = "Software Development",
                        PostedDate = DateTime.UtcNow,
                        ClosingDate = DateTime.UtcNow.AddDays(30)
                    }
                }
            };
            _mockJobService.Setup(s => s.GetListOfJobs(req)).Returns(expected);
            var result = _controller.GetListOfJobs(req) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.Value);
        }

        [Test]
        public void GetJobDetailsById_Found_ReturnsOk()
        {
            var expected = new JobDetailsDTO { Id = 1, Code = "JOB-01" };
            _mockJobService.Setup(s => s.GetJobDetailsById(1)).Returns(expected);
            var result = _controller.GetJobDetailsById(1) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.Value);
        }

        [Test]
        public void GetJobDetailsById_NotFound_ReturnsNotFound()
        {
            _mockJobService.Setup(s => s.GetJobDetailsById(99)).Returns((JobDetailsDTO?)null);
            var result = _controller.GetJobDetailsById(99);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
