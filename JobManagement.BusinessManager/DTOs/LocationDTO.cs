namespace JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs
{
    public class LocationCreateDTO
    {
        public string Title { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Zip { get; set; }
    }

    public class LocationDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Zip { get; set; }
    }
}
