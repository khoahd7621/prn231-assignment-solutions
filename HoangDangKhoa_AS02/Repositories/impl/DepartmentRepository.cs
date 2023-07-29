using BusinessObjects;
using DataAccess;

namespace Repositories.impl
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public void SaveDepartment(Department department)
            => DepartmentDao.SaveDepartment(department);

        public Department GetDepartmentById(int id)
            => DepartmentDao.FindDepartmentById(id);

        public List<Department> GetDepartments()
            => DepartmentDao.GetDepartments();

        public void UpdateDepartment(Department department)
            => DepartmentDao.UpdateDepartment(department);

        public void DeleteDepartment(Department department)
            => DepartmentDao.DeleteDepartment(department);
    }
}
