using BusinessObjects;

namespace DataAccess
{
    public class CustomerDAO
    {
        public static List<Customer> GetCustomers()
        {
            var listCustomers = new List<Customer>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listCustomers = context.Customers.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCustomers;
        }

        public static List<Customer> Search(string keyword)
        {
            var listCustomers = new List<Customer>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listCustomers = context.Customers
                        .Where(c => c.CustomerName.Contains(keyword))
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCustomers;
        }

        public static void SaveCustomer(Customer customer)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Customer FindCustomerById(string customerId)
        {
            var customer = new Customer();
            try
            {
                using (var context = new MyDBContext())
                {
                    customer = context.Customers.SingleOrDefault(c => c.Id == customerId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public static Customer FindCustomerByEmail(string email)
        {
            var customer = new Customer();
            try
            {
                using (var context = new MyDBContext())
                {
                    customer = context.Customers.FirstOrDefault(c => c.Email == email);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public static void UpdateCustomer(Customer customer)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Entry(customer).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCustomer(Customer customer)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var customerToDelete = context
                        .Customers
                        .SingleOrDefault(c => c.Id == customer.Id);
                    context.Customers.Remove(customerToDelete);
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