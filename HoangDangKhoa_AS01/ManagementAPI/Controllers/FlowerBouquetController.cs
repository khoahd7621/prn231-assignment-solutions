using BusinessObjects;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace ManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowerBouquetController : ControllerBase
    {
        private IFlowerBouquetRepository repository = new FlowerBouquetRepository();

        [HttpGet]
        public ActionResult<IEnumerable<FlowerBouquet>> GetFlowerBouquets() => repository.GetFlowerBouquets();

        [HttpGet("Search/{keyword}")]
        public ActionResult<IEnumerable<FlowerBouquet>> Search(string keyword) => repository.Search(keyword);

        [HttpGet("{id}")]
        public ActionResult<FlowerBouquet> GetFlowerBouquetById(int id) => repository.GetFlowerBouquetById(id);

        [HttpPost]
        public IActionResult PostFlowerBouquet(FlowerBouquetRequest flowerBouquetRequest)
        {
            var f = new FlowerBouquet
            {
                FlowerBouquetName = flowerBouquetRequest.FlowerBouquetName,
                Description = flowerBouquetRequest.Description,
                UnitPrice = flowerBouquetRequest.UnitPrice,
                UnitsInStock = flowerBouquetRequest.UnitsInStock,
                FlowerBouquetStatus = flowerBouquetRequest.FlowerBouquetStatus,
                CategoryID = flowerBouquetRequest.CategoryID,
                SupplierID = flowerBouquetRequest.SupplierID
            };
            repository.SaveFlowerBouquet(f);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFlowerBouquet(int id)
        {
            var f = repository.GetFlowerBouquetById(id);
            if (f == null)
            {
                return NotFound();
            }
            if (f.OrderDetails != null && f.OrderDetails.Count > 0)
            {
                f.FlowerBouquetStatus = 2;
                repository.UpdateFlowerBouquet(f);
            }
            else
            {
                repository.DeleteFlowerBouquet(f);
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutFlowerBouquet(int id, FlowerBouquetRequest flowerBouquetRequest)
        {
            var fTmp = repository.GetFlowerBouquetById(id);
            if (fTmp == null)
            {
                return NotFound();
            }

            fTmp.FlowerBouquetName = flowerBouquetRequest.FlowerBouquetName;
            fTmp.Description = flowerBouquetRequest.Description;
            fTmp.UnitPrice = flowerBouquetRequest.UnitPrice;
            fTmp.UnitsInStock = flowerBouquetRequest.UnitsInStock;
            fTmp.CategoryID = flowerBouquetRequest.CategoryID;
            fTmp.SupplierID = flowerBouquetRequest.SupplierID;

            repository.UpdateFlowerBouquet(fTmp);
            return NoContent();
        }
    }
}
