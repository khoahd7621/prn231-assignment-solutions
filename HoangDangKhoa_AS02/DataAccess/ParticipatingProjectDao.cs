using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ParticipatingProjectDao
    {
        public static List<ParticipatingProject> GetParticipatingProjects()
        {
            var listParticipatingProjects = new List<ParticipatingProject>();
            try
            {
                using (var context = new DBContext())
                {
                    listParticipatingProjects = context.ParticipatingProjects
                        .Include(s => s.Employee)
                        .Include(s => s.CompanyProject)
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listParticipatingProjects;
        }

        public static List<ParticipatingProject> FindAllParticipatingProjectsByCompanyProjectId(int companyProjectId)
        {
            var listParticipatingProjects = new List<ParticipatingProject>();
            try
            {
                using (var context = new DBContext())
                {
                    listParticipatingProjects = context.ParticipatingProjects
                        .Where(c => c.CompanyProjectID == companyProjectId)
                        .Include(s => s.Employee)
                        .Include(s => s.CompanyProject)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listParticipatingProjects;
        }

        public static List<ParticipatingProject> FindAllParticipatingProjectsByEmployeeId(int employeeId)
        {
            var listParticipatingProjects = new List<ParticipatingProject>();
            try
            {
                using (var context = new DBContext())
                {
                    listParticipatingProjects = context.ParticipatingProjects
                        .Where(c => c.EmployeeID == employeeId)
                        .Include(s => s.CompanyProject)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listParticipatingProjects;
        }

        public static ParticipatingProject FindParticipatingProjectByCompanyProjectIdAndEmployeeId(int companyProjectId, int employeeId)
        {
            var participatingProject = new ParticipatingProject();
            try
            {
                using (var context = new DBContext())
                {
                    participatingProject = context.ParticipatingProjects
                        .SingleOrDefault(c => c.CompanyProjectID == companyProjectId && c.EmployeeID == employeeId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return participatingProject;
        }

        public static void SaveParticipatingProject(ParticipatingProject participatingProject)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.ParticipatingProjects.Add(participatingProject);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateParticipatingProject(ParticipatingProject participatingProject)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Entry(participatingProject).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteParticipatingProject(ParticipatingProject participatingProject)
        {
            try
            {
                using (var context = new DBContext())
                {
                    var participatingProjectToDelete = context
                        .ParticipatingProjects
                        .SingleOrDefault(c => c.CompanyProjectID == participatingProject.CompanyProjectID && c.EmployeeID == participatingProject.EmployeeID);
                    context.ParticipatingProjects.Remove(participatingProjectToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
