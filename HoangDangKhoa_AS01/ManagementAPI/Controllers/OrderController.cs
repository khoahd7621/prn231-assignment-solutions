using BusinessObjects;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace ManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository repository = new OrderRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders() => repository.GetOrders();

        [HttpGet("customer/{id}")]
        public ActionResult<IEnumerable<Order>> GetAllOrdersByCustomerId(int id) => repository.GetAllOrdersByCustomerId(id);

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id) => repository.GetOrderById(id);

        [HttpPost]
        public ActionResult<Order> PostOrder(OrderReq orderReq)
        {
            var order = new Order
            {
                OrderDate = orderReq.OrderDate,
                ShippedDate = null,
                Total = orderReq.Total,
                OrderStatus = orderReq.OrderStatus,
                Freight = orderReq.Freight,
                CustomerID = orderReq.CustomerID
            };
            return repository.SaveOrder(order);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var o = repository.GetOrderById(id);
            if (o == null)
            {
                return NotFound();
            }
            repository.DeleteOrder(o);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, Order order)
        {
            var oTmp = repository.GetOrderById(id);
            if (oTmp == null)
            {
                return NotFound();
            }

            oTmp.OrderDate = order.OrderDate;
            oTmp.ShippedDate = order.ShippedDate;
            oTmp.Total = order.Total;
            oTmp.OrderStatus = order.OrderStatus;
            oTmp.Freight = order.Freight;
            oTmp.CustomerID = order.CustomerID;

            repository.UpdateOrder(oTmp);
            return NoContent();
        }

        [HttpPut("shipped/{id}")]
        public IActionResult PutOrderShipped(int id)
        {
            var oTmp = repository.GetOrderById(id);
            if (oTmp == null)
            {
                return NotFound();
            }
            oTmp.ShippedDate = DateTime.Now;
            oTmp.OrderStatus = 1;
            repository.UpdateOrder(oTmp);
            return NoContent();
        }

        [HttpPut("cancel/{id}")]
        public IActionResult PutOrderCancel(int id)
        {
            var oTmp = repository.GetOrderById(id);
            if (oTmp == null)
            {
                return NotFound();
            }
            oTmp.OrderStatus = 2;
            repository.UpdateOrder(oTmp);
            return NoContent();
        }
    }
}
