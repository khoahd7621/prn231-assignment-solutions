using BusinessObjects;

namespace Repositories
{
    public interface ICustomerRepository
    {
        void SaveCustomer(Customer customer);
        Customer GetCustomerById(int id);
        Customer GetCustomerByEmail(string email);
        List<Customer> GetCustomers();
        List<Customer> Search(string keyword);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        List<Order> GetOrders(int customerId);
    }
}
