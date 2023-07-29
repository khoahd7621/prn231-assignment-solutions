using BusinessObjects;
using FlowerBouquetWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace FlowerBouquetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository repository = new CustomerRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers() => repository.GetCustomers();

        [HttpGet("Search/{keyword}")]
        public ActionResult<IEnumerable<Customer>> Search(string keyword) => repository.Search(keyword);

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomerById(string id) => repository.GetCustomerById(id);

        [HttpGet("Email/{email}")]
        public ActionResult<Customer> GetCustomerByEmail(string email) => repository.GetCustomerByEmail(email);

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(string id)
        {
            var c = repository.GetCustomerById(id);
            if (c == null)
            {
                return NotFound();
            }
            repository.DeleteCustomer(c);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutCustomer(string id, PutCustomer putCustomer)
        {
            var cTmp = repository.GetCustomerById(id);
            if (cTmp == null)
            {
                return NotFound();
            }

            cTmp.CustomerName = putCustomer.CustomerName;
            cTmp.City = putCustomer.City;
            cTmp.Country = putCustomer.Country;
            cTmp.Birthday = putCustomer.Birthday;

            if (putCustomer.Password != null && cTmp.PasswordHash != putCustomer.Password)
            {
                cTmp.PasswordHash = putCustomer.Password;
            }

            repository.UpdateCustomer(cTmp);
            return NoContent();
        }
    }
}
