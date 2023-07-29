using BusinessObjects;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories;
using Repositories.impl;

namespace ProjectParticipantManagementSystemWebAPI.Controllers
{
    public class ParticipatingProjectsController : ODataController
    {
        private readonly IParticipatingProjectRepository repository = new ParticipatingProjectRepository();

        [EnableQuery]
        public IActionResult Get() => Ok(repository.GetParticipatingProjects());

        public ActionResult Post([FromBody] ParticipantPostRequest partiReq)
        {
            if (partiReq.StartDate > partiReq.EndDate)
                return BadRequest("Start date must be before end date.");

            ParticipatingProject parti = new ParticipatingProject
            {
                CompanyProjectID = partiReq.CompanyProjectID,
                EmployeeID = partiReq.EmployeeID,
                StartDate = partiReq.StartDate,
                EndDate = partiReq.EndDate,
                ProjectRole = partiReq.ProjectRole
            };

            repository.SaveParticipatingProject(parti);

            return Created(parti);
        }
    }
}
