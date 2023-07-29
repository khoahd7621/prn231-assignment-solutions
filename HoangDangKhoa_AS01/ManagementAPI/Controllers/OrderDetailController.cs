using BusinessObjects;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace ManagementAPI.Controllers
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
        public IActionResult PostOrderDetail(OrderDetailRequest OrderDetailRequest)
        {
            var orderDetail = new OrderDetail
            {
                OrderID = OrderDetailRequest.OrderID,
                FlowerBouquetID = OrderDetailRequest.FlowerBouquetID,
                UnitPrice = OrderDetailRequest.UnitPrice,
                Quantity = OrderDetailRequest.Quantity,
                Discount = OrderDetailRequest.Discount
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
