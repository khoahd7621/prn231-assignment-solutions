using BusinessObjects;
using BusinessObjects.Enums;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories;
using Repositories.impl;

namespace ProjectParticipantManagementSystemWebAPI.Controllers
{
    public class EmployeesController : ODataController
    {
        private readonly IEmployeeRepository repository = new EmployeeRepository();

        [EnableQuery]
        public IActionResult Get() => Ok(repository.GetEmployees());

        [EnableQuery]
        public ActionResult<Employee> Get([FromRoute] int key)
        {
            var item = repository.GetEmployeeById(key);

            if (item == null) return NotFound();

            return Ok(item);
        }

        public ActionResult Post([FromBody] EmployeeRequest empReq)
        {
            var tempEmp = repository.GetEmployeeByEmail(empReq.EmailAddress);

            if (tempEmp != null)
            {
                return BadRequest("Email address already exists.");
            }

            Employee employee = new Employee
            {
                FullName = empReq.FullName,
                EmailAddress = empReq.EmailAddress,
                Skills = empReq.Skills,
                Telephone = empReq.Telephone,
                Address = empReq.Address,
                Password = empReq.Password,
                DepartmentID = empReq.DepartmentID,
                Role = Role.Employee,
                Status = Status.Active
            };

            repository.SaveEmployee(employee);

            return Created(employee);
        }

        public IActionResult Put([FromRoute] int key, [FromBody] EmployeePutRequest empReq)
        {
            var employee = repository.GetEmployeeById(key);

            if (employee == null)
            {
                return NotFound();
            }

            employee.FullName = empReq.FullName;
            employee.Skills = empReq.Skills;
            employee.Telephone = empReq.Telephone;
            employee.Address = empReq.Address;
            employee.DepartmentID = empReq.DepartmentID;

            if (empReq.Password != null)
                employee.Password = empReq.Password;

            repository.UpdateEmployee(employee);

            return Updated(employee);
        }

        public ActionResult Delete([FromRoute] int key)
        {
            var employee = repository.GetEmployeeById(key);

            if (employee == null)
            {
                return NotFound();
            }

            if (employee.ParticipatingProjects.Count > 0)
            {
                return BadRequest("Employee is participating in a project.");
            }

            repository.DeleteEmployee(employee);
            return NoContent();
        }
    }
}
