namespace JobOpeningsManagementMS.Model
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<JobModel> Jobs { get; set; }
    }

}
