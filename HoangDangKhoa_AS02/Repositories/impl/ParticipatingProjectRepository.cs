using BusinessObjects;
using DataAccess;

namespace Repositories.impl
{
    public class ParticipatingProjectRepository : IParticipatingProjectRepository
    {
        public void SaveParticipatingProject(ParticipatingProject participatingProject)
            => ParticipatingProjectDao.SaveParticipatingProject(participatingProject);

        public ParticipatingProject GetParticipatingProjectByByCompanyProjectIdAndEmployeeId(int companyProjectId, int employeeId)
            => ParticipatingProjectDao.FindParticipatingProjectByCompanyProjectIdAndEmployeeId(companyProjectId, employeeId);

        public List<ParticipatingProject> GetParticipatingProjects()
            => ParticipatingProjectDao.GetParticipatingProjects();

        public List<ParticipatingProject> GetParticipatingProjectsByCompanyProjectId(int companyProjectId)
            => ParticipatingProjectDao.FindAllParticipatingProjectsByCompanyProjectId(companyProjectId);

        public List<ParticipatingProject> GetParticipatingProjectsByEmployeeId(int employeeId)
            => ParticipatingProjectDao.FindAllParticipatingProjectsByEmployeeId(employeeId);

        public void UpdateParticipatingProject(ParticipatingProject participatingProject)
            => ParticipatingProjectDao.UpdateParticipatingProject(participatingProject);

        public void DeleteParticipatingProject(ParticipatingProject participatingProject)
            => ParticipatingProjectDao.DeleteParticipatingProject(participatingProject);
    }
}
