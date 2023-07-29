using BusinessObjects;

namespace Repositories
{
    public interface IDepartmentRepository
    {
        void SaveDepartment(Department department);
        Department GetDepartmentById(int id);
        List<Department> GetDepartments();
        void UpdateDepartment(Department department);
        void DeleteDepartment(Department department);
    }
}
