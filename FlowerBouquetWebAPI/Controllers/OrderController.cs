using BusinessObjects;
using FlowerBouquetWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace FlowerBouquetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository repository = new OrderRepository();
        private IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        private IFlowerBouquetRepository flowerBouquetRepository = new FlowerBouquetRepository();

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders() => Ok(repository.GetOrders());

        [Authorize(Roles = UserRoles.Customer)]
        [HttpGet("customer/{id}")]
        public ActionResult<IEnumerable<Order>> GetAllOrdersByCustomerId(string id)
        {
            var listOrder = repository.GetAllOrdersByCustomerId(id);
            foreach (var o in listOrder)
            {
                var orderDetails = orderDetailRepository.GetOrderDetailsByOrderId(o.OrderID);
                o.OrderDetails = orderDetails;
            }
            return Ok(listOrder);
        }

        [Authorize(Roles = UserRoles.Customer)]
        [HttpGet("customer/detail/{id}")]
        public ActionResult<Order> GetOrderDetailById(int id)
        {
            var order = repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDetails = orderDetailRepository.GetOrderDetailsByOrderId(id);
            order.OrderDetails = orderDetails;
            return Ok(order);
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id) {
            var order = repository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDetails = orderDetailRepository.GetOrderDetailsByOrderId(id);
            order.OrderDetails = orderDetails;
            return Ok(order);
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpPost]
        public ActionResult<Order> PostOrder(PostOrder postOrder)
        {
            foreach (var od in postOrder.OrderDetails)
            { 
                var fb = flowerBouquetRepository.GetFlowerBouquetById(od.FlowerBouquetID);
                if (fb == null)
                {
                    return NotFound();
                }
                if (fb.FlowerBouquetStatus != 1)
                {
                    return BadRequest();
                }
                if (fb.UnitsInStock < od.Quantity)
                {
                    return BadRequest();
                }
            }
            var order = new Order
            {
                OrderDate = postOrder.OrderDate,
                ShippedDate = null,
                Total = postOrder.Total,
                OrderStatus = 0,
                Freight = postOrder.Freight,
                CustomerID = postOrder.CustomerID
            };
            var savedOrder = repository.SaveOrder(order);
            foreach (var od in postOrder.OrderDetails)
            {
                var fb = flowerBouquetRepository.GetFlowerBouquetById(od.FlowerBouquetID);
                fb.UnitsInStock -= od.Quantity;
                var orderDetail = new OrderDetail
                {
                    FlowerBouquetID = od.FlowerBouquetID,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    OrderID = savedOrder.OrderID,
                    Discount = 0
                };
                flowerBouquetRepository.UpdateFlowerBouquet(fb);
                orderDetailRepository.SaveOrderDetail(orderDetail);
            }
            return Ok(savedOrder);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("shipped/{id}")]
        public IActionResult PutOrderShipped(int id)
        {
            var oTmp = repository.GetOrderById(id);
            if (oTmp == null)
            {
                return NotFound();
            }
            if (oTmp.OrderStatus != 0)
            {
                return BadRequest();
            }
            oTmp.ShippedDate = DateTime.Now;
            oTmp.OrderStatus = 1;
            repository.UpdateOrder(oTmp);
            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("cancel/{id}")]
        public IActionResult PutOrderCancel(int id)
        {
            var oTmp = repository.GetOrderById(id);
            if (oTmp == null)
            {
                return NotFound();
            }
            if (oTmp.OrderStatus != 0)
            {
                return BadRequest();
            }
            oTmp.OrderStatus = 2;
            repository.UpdateOrder(oTmp);
            var orderDetails = orderDetailRepository.GetOrderDetailsByOrderId(id);
            foreach (var od in orderDetails)
            {
                var fb = flowerBouquetRepository.GetFlowerBouquetById(od.FlowerBouquetID);
                fb.UnitsInStock += od.Quantity;
                flowerBouquetRepository.UpdateFlowerBouquet(fb);
            }
            return NoContent();
        }
    }
}
