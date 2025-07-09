using JobOpeningsManagementMS.Data;
using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.Data.Interfaces;
using JobOpeningsManagementMS.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace JobOpeningsManagementMS.JobManagementAPI.Data.Implementation
{
    public class JobRepository : IJobRepository
    {
     
        private readonly AppDbContext _context;

        public JobRepository(AppDbContext context)
        {
            _context = context;
        }
        public JobModel Add(JobModel job)
        {
            _context.Jobs.Add(job);
            _context.SaveChanges();
            return job;
        }
        public JobModel Update(JobModel job)
        {
            _context.Jobs.Update(job);
            _context.SaveChanges();
            return job;
        }

        public bool Update(int id, CreateJobRequestDTO dto)
        {
            bool success = true;
            try
            {
                var job = _context.Jobs.FirstOrDefault(j => j.Id == id);

                if (job == null)
                    throw new Exception("Job not found");

                job.Title = dto.Title;
                job.Description = dto.Description;
                job.LocationId = dto.LocationId;
                job.DepartmentId = dto.DepartmentId;
                job.ClosingDate = dto.ClosingDate;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
            
        }

        public ListOfJobsResponseDTO GetListOfJobs(ListOfJobsRequestDTO request)
        {
            var query = _context.Jobs
                .Include(j => j.Location)
                .Include(j => j.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Q))
                query = query.Where(j => j.Title.Contains(request.Q));

            if (request.LocationId.HasValue)
                query = query.Where(j => j.LocationId == request.LocationId);

            if (request.DepartmentId.HasValue)
                query = query.Where(j => j.DepartmentId == request.DepartmentId);

            int total = query.Count();

            var jobs = query
                .OrderByDescending(j => j.PostedDate)
                .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(j => new JobResponseDTO
                {
                    Id = j.Id,
                    Code = j.Code,
                    Title = j.Title,
                    Location = j.Location.Title,
                    Department = j.Department.Title,
                    PostedDate = j.PostedDate,
                    ClosingDate = j.ClosingDate
                })
                .ToList();

            return new ListOfJobsResponseDTO
            {
                Total = total,
                Data = jobs
            };
        }
        public JobModel GetJobDetailsById(int id)
        {
            var job = _context.Jobs
                .Include(j => j.Location)
                .Include(j => j.Department)
                .FirstOrDefault(j => j.Id == id);

            if (job == null)
                return null;

            return new JobModel
            {
                Id = job.Id,
                Code = job.Code,
                Title = job.Title,
                Description = job.Description,
                PostedDate = job.PostedDate,
                ClosingDate = job.ClosingDate,
                Location = new LocationModel
                {
                    Id = job.Location.Id,
                    Title = job.Location.Title,
                    City = job.Location.City,
                    State = job.Location.State,
                    Country = job.Location.Country,
                    Zip = job.Location.Zip
                },
                Department = new DepartmentModel
                {
                    Id = job.Department.Id,
                    Title = job.Department.Title
                }
            };
        }



    }
}
