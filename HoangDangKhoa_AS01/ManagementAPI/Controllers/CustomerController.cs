using BusinessObjects;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace ManagementAPI.Controllers
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
        public ActionResult<Customer> GetCustomerById(int id) => repository.GetCustomerById(id);

        [HttpGet("Email/{email}")]
        public ActionResult<Customer> GetCustomerByEmail(string email) => repository.GetCustomerByEmail(email);

        [HttpPost]
        public IActionResult PostCustomer(CustomerRequest customerRequest)
        {
            var customer = new Customer
            {
                CustomerName = customerRequest.CustomerName,
                Email = customerRequest.Email,
                City = customerRequest.City,
                Country = customerRequest.Country,
                DateOfBirth = customerRequest.DateOfBirth,
                Password = customerRequest.Password,
                Role = "Customer"
            };
            repository.SaveCustomer(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
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
        public IActionResult PutCustomer(int id, CustomerRequest customerRequest)
        {
            var cTmp = repository.GetCustomerById(id);
            if (cTmp == null)
            {
                return NotFound();
            }

            cTmp.CustomerName = customerRequest.CustomerName;
            cTmp.Email = customerRequest.Email;
            cTmp.City = customerRequest.City;
            cTmp.Country = customerRequest.Country;
            cTmp.DateOfBirth = customerRequest.DateOfBirth;

            if (customerRequest.Password != null && cTmp.Password != customerRequest.Password)
            {
                cTmp.Password = customerRequest.Password;
            }

            repository.UpdateCustomer(cTmp);
            return NoContent();
        }
    }
}
