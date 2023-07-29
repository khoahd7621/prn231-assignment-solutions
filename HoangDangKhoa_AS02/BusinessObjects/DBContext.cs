using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects
{
    public class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        public virtual DbSet<CompanyProject> CompanyProjects { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<ParticipatingProject> ParticipatingProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyProject>().HasData(
                new CompanyProject { CompanyProjectID = 1, ProjectName = "Project 1", ProjectDescription = "Project 1 Description" },
                new CompanyProject { CompanyProjectID = 2, ProjectName = "Project 2", ProjectDescription = "Project 2 Description" },
                new CompanyProject { CompanyProjectID = 3, ProjectName = "Project 3", ProjectDescription = "Project 3 Description" }
                );

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentID = 1, DepartmentName = "Department 1", DepartmentDescription = "Department 1 Description" },
                new Department { DepartmentID = 2, DepartmentName = "Department 2", DepartmentDescription = "Department 2 Description" },
                new Department { DepartmentID = 3, DepartmentName = "Department 3", DepartmentDescription = "Department 3 Description" }
                );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeID = 1,
                    FullName = "Administrator",
                    Skills = "Manager",
                    Telephone = "123456789",
                    Address = "Address 1",
                    Status = Enums.Status.Active,
                    EmailAddress = "admin@gmail.com",
                    Password = "123456",
                    Role = Enums.Role.Admin,
                    DepartmentID = 1
                },
                new Employee
                {
                    EmployeeID = 2,
                    FullName = "Employee",
                    Skills = "Works",
                    Telephone = "123456789",
                    Address = "Address 2",
                    Status = Enums.Status.Active,
                    EmailAddress = "employee@gmail.com",
                    Password = "123456",
                    Role = Enums.Role.Employee,
                    DepartmentID = 1
                }
                );

            modelBuilder.Entity<ParticipatingProject>().HasData(
                new ParticipatingProject
                {
                    CompanyProjectID = 1,
                    EmployeeID = 2,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    ProjectRole = Enums.ProjectRole.ProjectManager
                }
                );
        }
    }
}
