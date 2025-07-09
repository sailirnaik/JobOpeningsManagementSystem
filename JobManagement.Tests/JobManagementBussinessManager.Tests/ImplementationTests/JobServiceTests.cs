using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Implementation;
using JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces;
using JobOpeningsManagementMS.Model;
using Moq;
using NUnit.Framework;

namespace JobOpeningsManagementMS.JobManagement.Tests.JobManagementBussinessManager.Tests.ImplementationTests
{
    [TestFixture]
    public class JobServiceTests
    {
        private Mock<IJobRepository> _jobRepositoryMock;
        private JobService _jobService;

        [SetUp]
        public void SetUp()
        {
            _jobRepositoryMock = new Mock<IJobRepository>();
            _jobService = new JobService(_jobRepositoryMock.Object);
        }

        
        [Test]
        public void UpdateJob_ShouldCallRepositoryAndReturnTrue()
        {
            var dto = new CreateJobRequestDTO { Title = "Updated Title" };

            _jobRepositoryMock.Setup(r => r.Update(1, dto)).Returns(true);

            var result = _jobService.UpdateJob(1, dto);

            Assert.IsTrue(result);
            _jobRepositoryMock.Verify(r => r.Update(1, dto), Times.Once);
        }

        [Test]
        public void GetListOfJobs_ShouldReturnResponse()
        {
            var dto = new ListOfJobsRequestDTO { PageNo = 1, PageSize = 10 };

            var expected = new ListOfJobsResponseDTO
            {
                Total = 1,
                Data = new List<JobResponseDTO>
                {
                    new JobResponseDTO { Id = 1, Title = "Job1", Code = "JOB-01" }
                }
            };

            _jobRepositoryMock.Setup(r => r.GetListOfJobs(dto)).Returns(expected);

            var result = _jobService.GetListOfJobs(dto);

            Assert.That(result.Total, Is.EqualTo(1));
            Assert.That(result.Data[0].Title, Is.EqualTo("Job1"));
        }

        [Test]
        public void GetJobDetailsById_ValidId_ShouldReturnJobDetails()
        {
            var job = new JobModel
            {
                Id = 1,
                Code = "JOB-01",
                Title = "Dev",
                Description = "Desc",
                PostedDate = DateTime.UtcNow,
                ClosingDate = DateTime.UtcNow.AddDays(10),
                Location = new LocationModel
                {
                    Id = 101,
                    Title = "Office",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    Zip = 12345
                },
                Department = new DepartmentModel
                {
                    Id = 201,
                    Title = "Dev Dept"
                }
            };

            _jobRepositoryMock.Setup(r => r.GetJobDetailsById(1)).Returns(job);

            var result = _jobService.GetJobDetailsById(1);

            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Location.Title, Is.EqualTo("Office"));
        }

        [Test]
        public void GetJobDetailsById_InvalidId_ShouldReturnNull()
        {
            _jobRepositoryMock.Setup(r => r.GetJobDetailsById(1)).Returns((JobModel)null!);

            var result = _jobService.GetJobDetailsById(1);

            Assert.IsNull(result);
        }
    }
}
