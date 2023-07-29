using BusinessObjects;

namespace Repositories
{
    public interface IOrderRepository
    {
        Order SaveOrder(Order order);
        Order GetOrderById(int id);
        List<Order> GetOrders();
        List<Order> GetAllOrdersByCustomerId(int customerId);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        List<OrderDetail> GetOrderDetails(int orderId);
    }
}
