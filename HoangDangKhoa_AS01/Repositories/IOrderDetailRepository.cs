using BusinessObjects;

namespace Repositories
{
    public interface IOrderDetailRepository
    {
        void SaveOrderDetail(OrderDetail orderDetail);
        OrderDetail GetOrderDetailByOrderIdAndFlowerBouquetId(int orderId, int flowerBouquetId);
        List<OrderDetail> GetOrderDetails();
        List<OrderDetail> GetOrderDetailsByOrderId(int orderId);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(OrderDetail orderDetail);
    }
}
