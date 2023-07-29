using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace ManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private ISupplierRepository repository = new SupplierRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Supplier>> GetSuppliers() => repository.GetSuppliers();

        [HttpGet("id")]
        public ActionResult<Supplier> GetSupplierById(int id) => repository.GetSupplierById(id);

        [HttpPost]
        public IActionResult PostSupplier(Supplier supplier)
        {
            repository.SaveSupplier(supplier);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteSupplier(int id)
        {
            var s = repository.GetSupplierById(id);
            if (s == null)
            {
                return NotFound();
            }
            repository.DeleteSupplier(s);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult PutSupplier(int id, Supplier supplier)
        {
            var sTmp = repository.GetSupplierById(id);
            if (sTmp == null)
            {
                return NotFound();
            }

            sTmp.SupplierName = supplier.SupplierName;
            sTmp.SupplierAddress = supplier.SupplierAddress;
            sTmp.Telephone = supplier.Telephone;

            repository.UpdateSupplier(sTmp);
            return NoContent();
        }
    }
}
