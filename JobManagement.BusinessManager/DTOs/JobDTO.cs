using JobOpeningsManagementMS.Model;

namespace JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs
{
    public class CreateJobRequestDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime ClosingDate { get; set; }
    }
    public class JobResponseDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
    public class ListOfJobsRequestDTO
    {
        public string Q { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LocationId { get; set; }
        public int? DepartmentId { get; set; }
    }
    public class ListOfJobsResponseDTO
    {
        public int Total { get; set; }
        public List<JobResponseDTO> Data { get; set; }
    }

    public class JobDetailsDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LocationModel Location { get; set; } = null!;
        public DepartmentModel Department { get; set; } = null!;
        public DateTime PostedDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}
