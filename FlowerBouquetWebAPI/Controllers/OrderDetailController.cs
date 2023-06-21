using BusinessObjects;
using FlowerBouquetWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace FlowerBouquetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private IOrderDetailRepository repository = new OrderDetailRepository();

        [HttpGet]
        public ActionResult<IEnumerable<OrderDetail>> GetOrderDetails() => repository.GetOrderDetails();
        [HttpGet("order/{id}")]
        public ActionResult<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int id) => repository.GetOrderDetailsByOrderId(id);

        [HttpGet("{orderId}/{flowerBouquetId}")]
        public ActionResult<OrderDetail> GetOrderDetailByOrderIdAndFlowerBouquetId(int orderId, int flowerBouquetId) => repository.GetOrderDetailByOrderIdAndFlowerBouquetId(orderId, flowerBouquetId);

        [HttpPost]
        public IActionResult PostOrderDetail(PostOrderDetail postOrderDetail)
        {
            var orderDetail = new OrderDetail
            {
                OrderID = postOrderDetail.OrderID,
                FlowerBouquetID = postOrderDetail.FlowerBouquetID,
                UnitPrice = postOrderDetail.UnitPrice,
                Quantity = postOrderDetail.Quantity,
                Discount = postOrderDetail.Discount
            };
            repository.SaveOrderDetail(orderDetail);
            return NoContent();
        }

        [HttpDelete("{orderId}/{flowerBouquetId}")]
        public IActionResult DeleteOrderDetailByOrderIdAndFlowerBouquetId(int orderId, int flowerBouquetId)
        {
            var o = repository.GetOrderDetailByOrderIdAndFlowerBouquetId(orderId, flowerBouquetId);
            if (o == null)
            {
                return NotFound();
            }
            repository.DeleteOrderDetail(o);
            return NoContent();
        }

        [HttpPut("{orderId}/{flowerBouquetId}")]
        public IActionResult PutOrderDetailByOrderIdAndFlowerBouquetId(int orderId, int flowerBouquetId, OrderDetail orderDetail)
        {
            var oTmp = repository.GetOrderDetailByOrderIdAndFlowerBouquetId(orderId, flowerBouquetId);
            if (oTmp == null)
            {
                return NotFound();
            }

            oTmp.UnitPrice = orderDetail.UnitPrice;
            oTmp.Quantity = orderDetail.Quantity;
            oTmp.Discount = orderDetail.Discount;

            repository.UpdateOrderDetail(oTmp);
            return NoContent();
        }
    }
}
