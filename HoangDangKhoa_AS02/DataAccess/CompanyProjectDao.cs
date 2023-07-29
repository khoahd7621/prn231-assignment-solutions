using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CompanyProjectDao
    {
        public static List<CompanyProject> GetCompanyProjects()
        {
            var listCompanyProjects = new List<CompanyProject>();
            try
            {
                using (var context = new DBContext())
                {
                    listCompanyProjects = context.CompanyProjects.Include(s => s.ParticipatingProjects).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCompanyProjects;
        }

        public static CompanyProject FindCompanyProjectById(int companyProjectID)
        {
            var companyProject = new CompanyProject();
            try
            {
                using (var context = new DBContext())
                {
                    companyProject = context.CompanyProjects
                        .Include(s => s.ParticipatingProjects)
                        .SingleOrDefault(c => c.CompanyProjectID == companyProjectID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return companyProject;
        }

        public static CompanyProject FindCompanyProjectByProjectName(string projectName)
        {
            var companyProject = new CompanyProject();
            try
            {
                using (var context = new DBContext())
                {
                    companyProject = context.CompanyProjects
                        .Include(s => s.ParticipatingProjects)
                        .SingleOrDefault(c => c.ProjectName.ToLower() == projectName.ToLower());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return companyProject;
        }

        public static void SaveCompanyProject(CompanyProject companyProject)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.CompanyProjects.Add(companyProject);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCompanyProject(CompanyProject companyProject)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Entry(companyProject).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCompanyProject(CompanyProject companyProject)
        {
            try
            {
                using (var context = new DBContext())
                {
                    var companyProjectToDelete = context
                        .CompanyProjects
                        .SingleOrDefault(c => c.CompanyProjectID == companyProject.CompanyProjectID);
                    context.CompanyProjects.Remove(companyProjectToDelete);
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