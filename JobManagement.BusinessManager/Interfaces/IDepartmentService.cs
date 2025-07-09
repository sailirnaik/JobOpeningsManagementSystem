using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.Model;

namespace JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Interfaces
{
    public interface IDepartmentService
    {
        void Create(DepartmentCreateDTO dto);
        void Update(int id, DepartmentCreateDTO dto);
        List<DepartmentDTO> GetAll();
    }
}
