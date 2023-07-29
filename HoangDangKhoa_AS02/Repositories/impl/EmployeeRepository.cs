using BusinessObjects;
using DataAccess;

namespace Repositories.impl
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public void SaveEmployee(Employee employee)
            => EmployeeDao.SaveEmployee(employee);

        public Employee GetEmployeeById(int id)
            => EmployeeDao.FindEmployeeById(id);

        public Employee GetEmployeeByEmail(string email)
            => EmployeeDao.FindEmployeeByEmail(email);

        public List<Employee> GetEmployees()
            => EmployeeDao.GetEmployees();

        public void UpdateEmployee(Employee employee)
            => EmployeeDao.UpdateEmployee(employee);

        public void DeleteEmployee(Employee employee)
            => EmployeeDao.DeleteEmployee(employee);
    }
}
