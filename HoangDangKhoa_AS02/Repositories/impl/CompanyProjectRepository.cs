using BusinessObjects;
using DataAccess;

namespace Repositories.impl
{
    public class CompanyProjectRepository : ICompanyProjectRepository
    {
        public void SaveCompanyProject(CompanyProject companyProject)
            => CompanyProjectDao.SaveCompanyProject(companyProject);

        public CompanyProject GetCompanyProjectById(int id)
            => CompanyProjectDao.FindCompanyProjectById(id);

        public CompanyProject GetCompanyProjectByProjectName(string projectName)
            => CompanyProjectDao.FindCompanyProjectByProjectName(projectName);

        public List<CompanyProject> GetCompanyProjects()
            => CompanyProjectDao.GetCompanyProjects();

        public void UpdateCompanyProject(CompanyProject companyProject)
            => CompanyProjectDao.UpdateCompanyProject(companyProject);

        public void DeleteCompanyProject(CompanyProject companyProject)
            => CompanyProjectDao.DeleteCompanyProject(companyProject);
    }
}
