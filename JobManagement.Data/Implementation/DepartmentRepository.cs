using JobOpeningsManagementMS.Data;
using JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces;
using JobOpeningsManagementMS.Model;
using Microsoft.EntityFrameworkCore;

namespace JobOpeningsManagementMS.JobManagementAPI.Data.Implementation
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(DepartmentModel department)
        {
            _context.Departments.Add(department);
        }

        public void Update(DepartmentModel department)
        {
            _context.Departments.Update(department);
        }

        public List<DepartmentModel> GetAll()
        {
            return _context.Departments.ToList();
        }

        public DepartmentModel? GetById(int id)
        {
            return _context.Departments.FirstOrDefault(d => d.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
