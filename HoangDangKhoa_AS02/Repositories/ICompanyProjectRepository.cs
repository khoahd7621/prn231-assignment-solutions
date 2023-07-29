using BusinessObjects;

namespace Repositories
{
    public interface ICompanyProjectRepository
    {
        void SaveCompanyProject(CompanyProject companyProject);
        CompanyProject GetCompanyProjectById(int id);
        CompanyProject GetCompanyProjectByProjectName(string projectName);
        List<CompanyProject> GetCompanyProjects();
        void UpdateCompanyProject(CompanyProject companyProject);
        void DeleteCompanyProject(CompanyProject companyProject);
    }
}