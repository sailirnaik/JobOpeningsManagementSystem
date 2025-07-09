using JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces;
using JobOpeningsManagementMS.Model;

namespace JobOpeningsManagementMS.Data.Implementation
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _context;

        public LocationRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(LocationModel location)
        {
            _context.Locations.Add(location);
        }

        public void Update(LocationModel location)
        {
            _context.Locations.Update(location);
        }

        public List<LocationModel> GetAll()
        {
            return _context.Locations.ToList();
        }

        public LocationModel? GetById(int id)
        {
            return _context.Locations.FirstOrDefault(l => l.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
