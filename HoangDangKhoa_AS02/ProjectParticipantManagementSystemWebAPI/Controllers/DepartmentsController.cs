using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories;
using Repositories.impl;

namespace ProjectParticipantManagementSystemWebAPI.Controllers
{
    public class DepartmentsController : ODataController
    {
        private readonly IDepartmentRepository repository = new DepartmentRepository();

        [EnableQuery]
        public IActionResult Get() => Ok(repository.GetDepartments());
    }
}
