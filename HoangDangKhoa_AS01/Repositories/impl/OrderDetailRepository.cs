using BusinessObjects;
using DataAccess;

namespace Repositories.impl
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void SaveOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.SaveOrderDetail(orderDetail);
        public OrderDetail GetOrderDetailByOrderIdAndFlowerBouquetId(int orderId, int flowerBouquetId) => OrderDetailDAO.FindOrderDetailByOrderIdAndFlowerBouquetId(orderId, flowerBouquetId);
        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.GetOrderDetails();
        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId) => OrderDetailDAO.FindAllOrderDetailsByOrderId(orderId);
        public void UpdateOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.UpdateOrderDetail(orderDetail);
        public void DeleteOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.DeleteOrderDetail(orderDetail);
    }
}
