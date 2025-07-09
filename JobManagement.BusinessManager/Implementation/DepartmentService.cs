using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Interfaces;
using JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces;
using JobOpeningsManagementMS.Model;

namespace JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public void Create(DepartmentCreateDTO dto)
        {
            var department = new DepartmentModel
            {
                Title = dto.Title
            };

            _repository.Add(department);
            _repository.Save();
        }

        public void Update(int id, DepartmentCreateDTO dto)
        {
            var department = _repository.GetById(id);
            if (department == null)
                throw new Exception("Department not found");

            department.Title = dto.Title;
            _repository.Update(department);
            _repository.Save();
        }

        public List<DepartmentDTO> GetAll()
        {
            var departments = _repository.GetAll();
            return departments.Select(d => new DepartmentDTO
            {
                Id = d.Id,
                Title = d.Title
            }).ToList();
        }
    }
}
