using BusinessObjects;
using BusinessObjects.Enums;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using ProjectParticipantManagementSystemWebClient.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectParticipantManagementSystemWebClient.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient client = null;
        private string EmployeeApiUrl = "";
        private string DepartmentApiUrl = "";

        public EmployeeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            EmployeeApiUrl = "http://localhost:5100/odata/Employees";
            DepartmentApiUrl = "http://localhost:5100/odata/Departments";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string email = HttpContext.Session.GetString("EMAIL");
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

            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + "?$expand=Department");
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Employee>>>(stringData);

            List<Employee> listEmps = oDataResponse.value;
            if (role == Role.Admin.ToString())
            {
                listEmps = listEmps.Where(e => !e.EmailAddress.Equals(email)).ToList();
            }

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["listEmployees"] = listEmps;

            return View();
        }

        public async Task<IActionResult> Search(string keyword)
        {
            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"?$filter=contains(tolower(FullName),tolower('{keyword}'))&$expand=Department");
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Employee>>>(stringData);

            ViewData["keyword"] = keyword;
            ViewData["listEmployees"] = oDataResponse.value;

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
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

            HttpResponseMessage response = await client.GetAsync(DepartmentApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Department>>>(stringData);

            ViewData["listDepartments"] = oDataResponse.value;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeRequest empReq)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid payload";
                return RedirectToAction("Create");
            }

            string stringData = JsonSerializer.Serialize(empReq);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(EmployeeApiUrl, contentData);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ErrorResponse errorRes = JsonSerializer.Deserialize<ErrorResponse>(responseData);
                TempData["ErrorMessage"] = errorRes.error.message;
                return RedirectToAction("Create");
            }

            TempData["SuccessMessage"] = "Create employee successfully.";
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

            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"?$filter=EmployeeID eq {id}&$expand=Department");
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Employee>>>(stringData);

            HttpResponseMessage responseDep = await client.GetAsync(DepartmentApiUrl);
            string stringDataDep = await responseDep.Content.ReadAsStringAsync();
            var oDataResponseDep = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Department>>>(stringDataDep);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["Employee"] = oDataResponse.value.FirstOrDefault();
            ViewData["listDepartments"] = oDataResponseDep.value;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeePutRequest empReq)
        {
            string stringData = JsonSerializer.Serialize(empReq);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(EmployeeApiUrl + $"({empReq.EmployeeID})", contentData);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ErrorResponse errorRes = JsonSerializer.Deserialize<ErrorResponse>(responseData);
                TempData["ErrorMessage"] = errorRes.error.message;
                return RedirectToAction("Edit", new { id = empReq.EmployeeID });
            }

            TempData["SuccessMessage"] = "Edit employee successfully.";
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
            int empId = HttpContext.Session.GetInt32("EMPID").Value;
            if (id == empId)
            {
                TempData["ErrorMessage"] = "You can't delete yourself.";
                return RedirectToAction("Index");
            }

            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"?$filter=EmployeeID eq {id}&$expand=Department");
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Employee>>>(stringData);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(oDataResponse.value.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string EmployeeID)
        {
            HttpResponseMessage response = await client.DeleteAsync(EmployeeApiUrl + $"({EmployeeID})");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Delete employee successfully.";
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
                TempData["ErrorMessage"] = "Delete employee failed.";
            }
            return RedirectToAction("Delete", new { id = EmployeeID });
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            string Role = HttpContext.Session.GetString("ROLE");
            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            int empId = HttpContext.Session.GetInt32("EMPID").Value;

            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"?$filter=EmployeeID eq {empId}&$expand=Department");
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Employee>>>(stringData);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(oDataResponse.value.FirstOrDefault());
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            int empId = HttpContext.Session.GetInt32("EMPID").Value;

            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"?$filter=EmployeeID eq {empId}&$expand=Department");
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Employee>>>(stringData);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            ViewData["employee"] = oDataResponse.value.FirstOrDefault();

            if (role == Role.Admin.ToString())
            {
                HttpResponseMessage responseDep = await client.GetAsync(DepartmentApiUrl);
                string stringDataDep = await responseDep.Content.ReadAsStringAsync();
                var oDataResponseDep = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<OData<List<Department>>>(stringDataDep);
                ViewData["listDepartments"] = oDataResponseDep.value;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EmployeePutRequest empReq)
        {
            int? empId = HttpContext.Session.GetInt32("EMPID");
            if (empId == null)
                return RedirectToAction("Index", "Home");

            empReq.EmployeeID = empId.Value;
            string stringData = JsonSerializer.Serialize(empReq);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(EmployeeApiUrl + $"({empReq.EmployeeID})", contentData);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                ErrorResponse errorRes = JsonSerializer.Deserialize<ErrorResponse>(responseData);
                TempData["ErrorMessage"] = errorRes.error.message;
                return RedirectToAction("Profile");
            }

            TempData["SuccessMessage"] = "Edit profile information successfully.";
            return RedirectToAction("Profile", TempData);
        }
    }
}
