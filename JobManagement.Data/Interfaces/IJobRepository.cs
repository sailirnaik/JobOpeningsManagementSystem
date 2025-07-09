using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.Model;

namespace JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces
{
    public interface IJobRepository
    {
        JobModel  Add(JobModel job);
        JobModel Update(JobModel job);
        bool Update(int id, CreateJobRequestDTO dto);
        ListOfJobsResponseDTO GetListOfJobs(ListOfJobsRequestDTO dto);
        JobModel GetJobDetailsById(int id);
    }
}
