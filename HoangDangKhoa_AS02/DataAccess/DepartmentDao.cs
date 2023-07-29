using BusinessObjects;

namespace DataAccess
{
    public class DepartmentDao
    {
        public static List<Department> GetDepartments()
        {
            var listDepartments = new List<Department>();
            try
            {
                using (var context = new DBContext())
                {
                    listDepartments = context.Departments.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listDepartments;
        }

        public static Department FindDepartmentById(int departmentID)
        {
            var department = new Department();
            try
            {
                using (var context = new DBContext())
                {
                    department = context.Departments.SingleOrDefault(c => c.DepartmentID == departmentID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return department;
        }

        public static void SaveDepartment(Department department)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Departments.Add(department);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateDepartment(Department department)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.Entry(department).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteDepartment(Department department)
        {
            try
            {
                using (var context = new DBContext())
                {
                    var departmentToDelete = context
                        .Departments
                        .SingleOrDefault(c => c.DepartmentID == department.DepartmentID);
                    context.Departments.Remove(departmentToDelete);
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
