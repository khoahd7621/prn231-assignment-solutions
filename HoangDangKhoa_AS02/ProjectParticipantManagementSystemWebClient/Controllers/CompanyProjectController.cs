using BusinessObjects;
using BusinessObjects.Enums;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using ProjectParticipantManagementSystemWebClient.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectParticipantManagementSystemWebClient.Controllers
{
    public class CompanyProjectController : Controller
    {
        private readonly HttpClient client = null;
        private string CompanyProjectApiUrl = "";
        private string EmployeeApiUrl = "";
        private string ParticipationApiUrl = "";

        public CompanyProjectController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CompanyProjectApiUrl = "http://localhost:5100/odata/CompanyProjects";
            EmployeeApiUrl = "http://localhost:5100/odata/Employees";
            ParticipationApiUrl = "http://localhost:5100/odata/ParticipatingProjects";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string role = HttpContext.Session.GetString("ROLE");

            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (role != Role.Admin.ToString())
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Employee");
            }

            HttpResponseMessage response = await client.GetAsync(CompanyProjectApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<CompanyProject>>>(stringData);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["listCompanyProjects"] = oDataResponse.value;

            return View();
        }

        public async Task<IActionResult> Search(string keyword)
        {
            HttpResponseMessage response = await client.GetAsync(CompanyProjectApiUrl + $"?$filter=contains(tolower(ProjectName),tolower('{keyword}'))");
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<CompanyProject>>>(stringData);

            ViewData["keyword"] = keyword;
            ViewData["listCompanyProjects"] = oDataResponse.value;

            return View("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            string role = HttpContext.Session.GetString("ROLE");

            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (role != Role.Admin.ToString())
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Employee");
            }

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyProjectPostRequest cpnPrjReq)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid payload";
                return RedirectToAction("Create");
            }

            string stringData = JsonSerializer.Serialize(cpnPrjReq);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(CompanyProjectApiUrl, contentData);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ErrorResponse errorRes = JsonSerializer.Deserialize<ErrorResponse>(responseData);
                TempData["ErrorMessage"] = errorRes.error.message;
                return RedirectToAction("Create");
            }

            TempData["SuccessMessage"] = "Create company project successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string role = HttpContext.Session.GetString("ROLE");

            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (role != Role.Admin.ToString())
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Employee");
            }

            HttpResponseMessage response = await client.GetAsync(CompanyProjectApiUrl + $"?$filter=CompanyProjectID eq {id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<CompanyProject>>>(stringData);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["companyProject"] = oDataResponse.value.FirstOrDefault();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyProjectPutRequest cpnPrjReq)
        {
            string stringData = JsonSerializer.Serialize(cpnPrjReq);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(CompanyProjectApiUrl + $"({cpnPrjReq.CompanyProjectID})", contentData);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ErrorResponse errorRes = JsonSerializer.Deserialize<ErrorResponse>(responseData);
                TempData["ErrorMessage"] = errorRes.error.message;
                return RedirectToAction("Edit", new { id = cpnPrjReq.CompanyProjectID });
            }

            TempData["SuccessMessage"] = "Edit company project successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string role = HttpContext.Session.GetString("ROLE");

            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (role != Role.Admin.ToString())
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Employee");
            }

            HttpResponseMessage response = await client.GetAsync(CompanyProjectApiUrl + $"?$filter=CompanyProjectID eq {id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<CompanyProject>>>(stringData);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(oDataResponse.value.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string CompanyProjectID)
        {
            HttpResponseMessage response = await client.DeleteAsync(CompanyProjectApiUrl + $"({CompanyProjectID})");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Delete company project successfully.";
                return RedirectToAction("Index");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ErrorResponse errorRes = JsonSerializer.Deserialize<ErrorResponse>(responseData);
                TempData["ErrorMessage"] = errorRes.error.message;
            }
            else
            {
                TempData["ErrorMessage"] = "Delete company project failed.";
            }
            return RedirectToAction("Delete", new { id = CompanyProjectID });
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            string role = HttpContext.Session.GetString("ROLE");

            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (role != Role.Admin.ToString())
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Employee");
            }

            HttpResponseMessage participantResMsg = await client.GetAsync(ParticipationApiUrl + $"?$filter=CompanyProjectID eq {id}&$expand=Employee");
            string participantStrData = await participantResMsg.Content.ReadAsStringAsync();
            var participantODataRes = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<ParticipatingProject>>>(participantStrData);

            HttpResponseMessage empResMsg = await client.GetAsync(EmployeeApiUrl);
            string empStrData = await empResMsg.Content.ReadAsStringAsync();
            var empODataRes = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Employee>>>(empStrData);

            List<Employee> listEmployees = empODataRes.value
                .Where(emp => emp.Role != Role.Admin)
                .Where(emp => participantODataRes.value.FirstOrDefault(p => p.Employee.EmployeeID == emp.EmployeeID) == null)
                .ToList();

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["CompanyProjectID"] = id;
            ViewData["listEmployees"] = listEmployees;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ParticipantPostRequest partReq)
        {
            string stringData = JsonSerializer.Serialize(partReq);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(ParticipationApiUrl, contentData);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ErrorResponse errorRes = JsonSerializer.Deserialize<ErrorResponse>(responseData);
                TempData["ErrorMessage"] = errorRes.error.message;
                return RedirectToAction("Add", new { id = partReq.CompanyProjectID });
            }

            TempData["SuccessMessage"] = "Add participant successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ProjectParticipant(int id)
        {
            string role = HttpContext.Session.GetString("ROLE");

            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (role != Role.Admin.ToString())
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Employee");
            }

            HttpResponseMessage response = await client.GetAsync(CompanyProjectApiUrl + $"?$filter=CompanyProjectID eq {id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<CompanyProject>>>(stringData);

            CompanyProject companyProject = oDataResponse.value.FirstOrDefault();

            if (companyProject == null)
            {
                return RedirectToAction("Index");
            }

            HttpResponseMessage response2 = await client.GetAsync(ParticipationApiUrl + $"?$filter=CompanyProjectID eq {id}&$expand=Employee");
            string stringData2 = await response2.Content.ReadAsStringAsync();
            var oDataResponse2 = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<ParticipatingProject>>>(stringData2);

            ViewData["companyProject"] = companyProject;
            ViewData["listParticipants"] = oDataResponse2.value;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MyProject()
        {
            string role = HttpContext.Session.GetString("ROLE");

            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (role != Role.Employee.ToString())
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Index", "Employee");
            }

            int employeeID = HttpContext.Session.GetInt32("EMPID").Value;

            HttpResponseMessage response = await client.GetAsync(ParticipationApiUrl + $"?$filter=EmployeeID eq {employeeID}&$expand=CompanyProject");
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<ParticipatingProject>>>(stringData);

            ViewData["listParticipants"] = oDataResponse.value;

            return View();
        }
    }
}
