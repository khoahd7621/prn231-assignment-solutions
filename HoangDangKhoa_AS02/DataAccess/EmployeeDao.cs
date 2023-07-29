using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class EmployeeDao
    {
        public static List<Employee> GetEmployees()
        {
            var listEmployees = new List<Employee>();
            try
            {
                using (var context = new DBContext())
                {
                    listEmployees = context.Employees
                        .Include(s => s.Department)
                        .Include(s => s.ParticipatingProjects)
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listEmployees;
        }

        public static Employee FindEmployeeById(int employeeID)
        {
            var employee = new Employee();
            try
            {
                using (var context = new DBContext())
                {
                    employee = context.Employees
                        .Include(s => s.Department)
                        .Include(s => s.ParticipatingProjects)
                        .SingleOrDefault(c => c.EmployeeID == employeeID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return employee;
        }

        public static Employee FindEmployeeByEmail(string email)
        {
            var employee = new Employee();
            try
            {
                using (var context = new DBContext())
                {
                    employee = context.Employees
                        .Include(s => s.Department)
                        .Include(s => s.ParticipatingProjects)
                        .SingleOrDefault(c => c.EmailAddress == email);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return employee;
        }

        public static void SaveEmployee(Employee employee)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Employees.Add(employee);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateEmployee(Employee employee)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Entry(employee).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteEmployee(Employee employee)
        {
            try
            {
                using (var context = new DBContext())
                {
                    var employeeToDelete = context
                        .Employees
                        .SingleOrDefault(c => c.EmployeeID == employee.EmployeeID);
                    context.Employees.Remove(employeeToDelete);
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
