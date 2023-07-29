using BusinessObjects;
using FlowerBouquetWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace FlowerBouquetWebAPI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private ISupplierRepository repository = new SupplierRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Supplier>> GetSuppliers() => repository.GetSuppliers();

        [HttpPost]
        public IActionResult PostSupplier(Supplier supplier)
        {
            repository.SaveSupplier(supplier);
            return NoContent();
        }
    }
}
