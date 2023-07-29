using BusinessObjects;

namespace DataAccess
{
    public class OrderDAO
    {
        public static List<Order> GetOrders()
        {
            var listOrders = new List<Order>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listOrders = context.Orders.ToList();
                    listOrders.ForEach(o => o.Customer = context.Customers.Find(o.CustomerID));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrders;
        }

        public static List<Order> FindAllOrdersByCustomerId(string customerId)
        {
            var listOrders = new List<Order>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listOrders = context.Orders.Where(o => o.CustomerID == customerId).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrders;
        }

        public static Order FindOrderById(int orderId)
        {
            var order = new Order();
            try
            {
                using (var context = new MyDBContext())
                {
                    order = context.Orders.SingleOrDefault(o => o.OrderID == orderId);
                    if (order != null)
                        order.Customer = context.Customers.Find(order.CustomerID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public static Order SaveOrder(Order order)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public static void UpdateOrder(Order order)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Entry(order).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrder(Order order)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var orderToDelete = context
                        .Orders
                        .SingleOrDefault(o => o.OrderID == order.OrderID);
                    context.Orders.Remove(orderToDelete);
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
