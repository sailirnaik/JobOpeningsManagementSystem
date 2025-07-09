using Microsoft.CodeAnalysis;

namespace JobOpeningsManagementMS.Model
{
    public class JobModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public LocationModel Location { get; set; } = null!;
        public int DepartmentId { get; set; }
        public DepartmentModel Department { get; set; } = null!;
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;
        public DateTime ClosingDate { get; set; }


    }
}
