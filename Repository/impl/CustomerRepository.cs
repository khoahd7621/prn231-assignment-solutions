using BusinessObjects;
using DataAccess;

namespace Repositories.impl
{
    public class CustomerRepository : ICustomerRepository
    {
        public void SaveCustomer(Customer customer) => CustomerDAO.SaveCustomer(customer);
        public List<Customer> GetCustomers() => CustomerDAO.GetCustomers();
        public List<Customer> Search(string keyword) => CustomerDAO.Search(keyword);
        public Customer GetCustomerById(string id) => CustomerDAO.FindCustomerById(id);
        public Customer GetCustomerByEmail(string email) => CustomerDAO.FindCustomerByEmail(email);
        public void UpdateCustomer(Customer customer) => CustomerDAO.UpdateCustomer(customer);
        public void DeleteCustomer(Customer customer) => CustomerDAO.DeleteCustomer(customer);
        public List<Order> GetOrders(string customerId) => OrderDAO.FindAllOrdersByCustomerId(customerId);
    }
}
