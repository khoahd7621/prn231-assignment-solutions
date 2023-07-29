using BusinessObjects;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetOrderDetails()
        {
            var listOrderDetails = new List<OrderDetail>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listOrderDetails = context.OrderDetails.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrderDetails;
        }

        public static List<OrderDetail> FindAllOrderDetailsByFlowerBouquetId(int flowerBouquetId)
        {
            var listOrderDetails = new List<OrderDetail>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listOrderDetails = context
                        .OrderDetails
                        .Where(o => o.FlowerBouquetID == flowerBouquetId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrderDetails;
        }

        public static List<OrderDetail> FindAllOrderDetailsByOrderId(int orderId)
        {
            var listOrderDetails = new List<OrderDetail>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listOrderDetails = context
                        .OrderDetails
                        .Where(o => o.OrderID == orderId)
                        .ToList();
                    listOrderDetails.ForEach(o =>
                        o.FlowerBouquet = context.FlowerBouquets.SingleOrDefault(f => f.FlowerBouquetID == o.FlowerBouquetID)
                    );
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrderDetails;
        }

        public static OrderDetail FindOrderDetailByOrderIdAndFlowerBouquetId(int orderId, int flowerBouquetId)
        {
            var orderDetail = new OrderDetail();
            try
            {
                using (var context = new MyDBContext())
                {
                    orderDetail = context
                        .OrderDetails
                        .SingleOrDefault(o => o.OrderID == orderId && o.FlowerBouquetID == flowerBouquetId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        public static void SaveOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Entry(orderDetail).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var orderDetailToDelete = context
                        .OrderDetails
                        .SingleOrDefault(o => o.OrderID == orderDetail.OrderID && o.FlowerBouquetID == orderDetail.FlowerBouquetID);
                    context.OrderDetails.Remove(orderDetailToDelete);
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
