using BusinessObjects;

namespace Repositories
{
    public interface IEmployeeRepository
    {
        void SaveEmployee(Employee employee);
        Employee GetEmployeeById(int id);
        Employee GetEmployeeByEmail(string email);
        List<Employee> GetEmployees();
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
