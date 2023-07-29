using BusinessObjects;

namespace Repositories
{
    public interface IParticipatingProjectRepository
    {
        void SaveParticipatingProject(ParticipatingProject participatingProject);
        ParticipatingProject GetParticipatingProjectByByCompanyProjectIdAndEmployeeId(int companyProjectId, int employeeId);
        List<ParticipatingProject> GetParticipatingProjects();
        List<ParticipatingProject> GetParticipatingProjectsByCompanyProjectId(int companyProjectId);
        List<ParticipatingProject> GetParticipatingProjectsByEmployeeId(int employeeId);
        void UpdateParticipatingProject(ParticipatingProject participatingProject);
        void DeleteParticipatingProject(ParticipatingProject participatingProject);
    }
}
