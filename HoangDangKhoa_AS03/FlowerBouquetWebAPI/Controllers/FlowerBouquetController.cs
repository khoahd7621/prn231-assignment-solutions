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
    public class FlowerBouquetController : ControllerBase
    {
        private IFlowerBouquetRepository repository = new FlowerBouquetRepository();

        [HttpGet]
        public ActionResult<IEnumerable<FlowerBouquet>> GetFlowerBouquets() => repository.GetFlowerBouquets();

        [HttpGet("Search/{keyword}")]
        public ActionResult<IEnumerable<FlowerBouquet>> Search(string keyword) => repository.Search(keyword);

        [HttpGet("{id}")]
        public ActionResult<FlowerBouquet> GetFlowerBouquetById(int id) => repository.GetFlowerBouquetById(id);

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public IActionResult PostFlowerBouquet(PostFlowerBouquet postFlowerBouquet)
        {
            if (repository.GetFlowerBouquets().FirstOrDefault(f => f.FlowerBouquetName.ToLower().Equals(postFlowerBouquet.FlowerBouquetName.ToLower())) != null) {
                return BadRequest();
            }
            var f = new FlowerBouquet
            {
                FlowerBouquetName = postFlowerBouquet.FlowerBouquetName,
                Description = postFlowerBouquet.Description,
                UnitPrice = postFlowerBouquet.UnitPrice,
                UnitsInStock = postFlowerBouquet.UnitsInStock,
                FlowerBouquetStatus = postFlowerBouquet.FlowerBouquetStatus,
                CategoryID = postFlowerBouquet.CategoryID,
                SupplierID = postFlowerBouquet.SupplierID
            };
            repository.SaveFlowerBouquet(f);
            return NoContent();
        }

        [Authorize(Roles = UserRoles.Admin)]
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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut("{id}")]
        public IActionResult PutFlowerBouquet(int id, PostFlowerBouquet postFlowerBouquet)
        {
            var fTmp = repository.GetFlowerBouquetById(id);
            if (fTmp == null)
            {
                return NotFound();
            }

            if (!fTmp.FlowerBouquetName.ToLower().Equals(postFlowerBouquet.FlowerBouquetName.ToLower()) 
                && repository.GetFlowerBouquets().FirstOrDefault(f => f.FlowerBouquetName.ToLower().Equals(postFlowerBouquet.FlowerBouquetName.ToLower())) != null)
            {
                return BadRequest();
            } else
            {
                fTmp.FlowerBouquetName = postFlowerBouquet.FlowerBouquetName;
            }

            fTmp.Description = postFlowerBouquet.Description;
            fTmp.UnitPrice = postFlowerBouquet.UnitPrice;
            fTmp.UnitsInStock = postFlowerBouquet.UnitsInStock;
            fTmp.CategoryID = postFlowerBouquet.CategoryID;
            fTmp.SupplierID = postFlowerBouquet.SupplierID;

            repository.UpdateFlowerBouquet(fTmp);
            return NoContent();
        }
    }
}
