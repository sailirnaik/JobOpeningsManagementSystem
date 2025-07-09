using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.Model;

namespace JobOpeningsManagementMS.Services
{
    public interface IJobService
    {
       int CreateJob(CreateJobRequestDTO dto);
       bool UpdateJob(int id, CreateJobRequestDTO dto);
        ListOfJobsResponseDTO GetListOfJobs(ListOfJobsRequestDTO dto);
        JobDetailsDTO GetJobDetailsById(int id);
    }
}
