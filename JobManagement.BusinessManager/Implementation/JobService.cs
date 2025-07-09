using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces;
using JobOpeningsManagementMS.Model;
using JobOpeningsManagementMS.Services;

namespace JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Implementation
{
    public class JobService : IJobService
    {
      

        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        int IJobService.CreateJob(CreateJobRequestDTO dto)
        {
            if (dto != null)
            {
                var newJob = new JobModel
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    LocationId = dto.LocationId,
                    DepartmentId = dto.DepartmentId,
                    ClosingDate = dto.ClosingDate,
                    PostedDate = DateTime.UtcNow
                };

                JobModel insertedJob = _jobRepository.Add(newJob);
                insertedJob.Code = $"JOB-{insertedJob.Id:D2}";
                insertedJob = _jobRepository.Update(insertedJob);

                return insertedJob.Id;
            }
            return -1;
        }

        public bool UpdateJob(int id, CreateJobRequestDTO dto)
        {
            bool isSuccess = _jobRepository.Update(id, dto);
            return isSuccess;
        }


        public ListOfJobsResponseDTO GetListOfJobs(ListOfJobsRequestDTO dto)
        {
            return _jobRepository.GetListOfJobs(dto);
        }
        public JobDetailsDTO GetJobDetailsById(int id)
        {
            JobModel job = _jobRepository.GetJobDetailsById(id);
            if (job != null)
            {
                var jobdetails = new JobDetailsDTO
                {
                    Id = job.Id,
                    Code = job.Code,
                    Title = job.Title,
                    Description = job.Description,
                    PostedDate = job.PostedDate,
                    ClosingDate = job.ClosingDate,
                    Location = new LocationModel
                    {
                        Id = job.Location.Id,
                        Title = job.Location.Title,
                        City = job.Location.City,
                        State = job.Location.State,
                        Country = job.Location.Country,
                        Zip = job.Location.Zip
                    },
                    Department = new DepartmentModel
                    {
                        Id = job.Department.Id,
                        Title = job.Department.Title
                    }
                };
                return jobdetails;
            }
                return null;
        }

    }
}
