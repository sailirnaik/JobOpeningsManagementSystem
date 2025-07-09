using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobOpeningsManagementMS.Model;
using Microsoft.EntityFrameworkCore;

namespace JobOpeningsManagementMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<JobModel> Jobs { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobModel>()
                .HasOne(j => j.Department)
                .WithMany(d => d.Jobs)
                .HasForeignKey(j => j.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobModel>()
                .HasOne(j => j.Location)
                .WithMany(l => l.Jobs)
                .HasForeignKey(j => j.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobModel>()
               .ToTable("JobManagementTable");

            modelBuilder.Entity<LocationModel>()
                .ToTable("LocationTable");

            modelBuilder.Entity<DepartmentModel>()
             .ToTable("DepartmentTable");

        }
    }
}
