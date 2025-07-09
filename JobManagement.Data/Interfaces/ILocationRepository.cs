using JobOpeningsManagementMS.Model;

namespace JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces
{
    public interface ILocationRepository
    {
        void Add(LocationModel location);
        void Update(LocationModel location);
        List<LocationModel> GetAll();
        LocationModel? GetById(int id);
        void Save();
    }
}
