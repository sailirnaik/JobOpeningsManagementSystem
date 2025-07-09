using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.Model;

namespace JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Interfaces
{
    public interface ILocationService
    {
        void Create(LocationCreateDTO dto);
        void Update(int id, LocationCreateDTO dto);
        List<LocationDTO> GetAll();
    }
}
