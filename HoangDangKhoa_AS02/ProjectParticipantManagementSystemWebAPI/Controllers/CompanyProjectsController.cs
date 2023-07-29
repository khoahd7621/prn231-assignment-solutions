using BusinessObjects;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories;
using Repositories.impl;

namespace ProjectParticipantManagementSystemWebAPI.Controllers
{
    public class CompanyProjectsController : ODataController
    {
        private readonly ICompanyProjectRepository repository = new CompanyProjectRepository();

        [EnableQuery]
        public IActionResult Get() => Ok(repository.GetCompanyProjects());

        [EnableQuery]
        public ActionResult<CompanyProject> Get([FromRoute] int key)
        {
            var item = repository.GetCompanyProjectById(key);

            if (item == null) return NotFound();

            return Ok(item);
        }

        public ActionResult Post([FromBody] CompanyProjectPostRequest cpnPrjReq)
        {
            var tempCpnPrj = repository.GetCompanyProjectByProjectName(cpnPrjReq.ProjectName);

            if (tempCpnPrj != null)
            {
                return BadRequest("Project name already exists.");
            }

            CompanyProject cpnPrj = new CompanyProject
            {
                ProjectName = cpnPrjReq.ProjectName,
                ProjectDescription = cpnPrjReq.ProjectDescription
            };

            repository.SaveCompanyProject(cpnPrj);

            return Created(cpnPrj);
        }

        public IActionResult Put([FromRoute] int key, [FromBody] CompanyProjectPutRequest cpnPrjReq)
        {
            var cpnPrj = repository.GetCompanyProjectById(key);

            if (cpnPrj == null)
            {
                return NotFound();
            }

            if (cpnPrjReq.ProjectName != cpnPrj.ProjectName)
            {
                var tempCpnPrj = repository.GetCompanyProjectByProjectName(cpnPrjReq.ProjectName);

                if (tempCpnPrj != null)
                {
                    return BadRequest("Project name already exists.");
                }

                cpnPrj.ProjectName = cpnPrjReq.ProjectName;
            }


            cpnPrj.ProjectDescription = cpnPrjReq.ProjectDescription;

            repository.UpdateCompanyProject(cpnPrj);

            return Updated(cpnPrj);
        }

        public ActionResult Delete([FromRoute] int key)
        {
            var cpnPrj = repository.GetCompanyProjectById(key);

            if (cpnPrj == null)
            {
                return NotFound();
            }

            if (cpnPrj.ParticipatingProjects.Count > 0)
            {
                return BadRequest("Cannot delete company project that has participating projects.");
            }

            repository.DeleteCompanyProject(cpnPrj);
            return NoContent();
        }
    }
}
