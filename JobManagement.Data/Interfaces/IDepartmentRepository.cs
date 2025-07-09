using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.Model;

namespace JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces
{
    public interface IDepartmentRepository
    {
        void Add(DepartmentModel department);
        void Update(DepartmentModel department);
        List<DepartmentModel> GetAll();
        DepartmentModel? GetById(int id);
        void Save();
    }
}
