using BusinessObjects;
using BusinessObjects.Enums;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using ProjectParticipantManagementSystemWebClient.Models;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace ProjectParticipantManagementSystemWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client = null;
        private string EmployeeApiUrl = "";

        public HomeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            EmployeeApiUrl = "http://localhost:5100/odata/Employees";
        }

        [HttpGet]
        public IActionResult Index()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role == Role.Admin.ToString())
                return RedirectToAction("Index", "Employee");
            else if (role == Role.Employee.ToString())
                return RedirectToAction("Profile", "Employee");

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest loginRequest)
        {
            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();
            var oDataResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<OData<List<Employee>>>(stringData);

            Employee emp = oDataResponse.value
                .Where(e => e.EmailAddress == loginRequest.Email && e.Password == loginRequest.Password)
                .FirstOrDefault();

            if (emp != null)
            {
                HttpContext.Session.SetInt32("EMPID", emp.EmployeeID);
                HttpContext.Session.SetString("EMAIL", emp.EmailAddress);
                HttpContext.Session.SetString("FULLNAME", emp.FullName);
                HttpContext.Session.SetString("ROLE", emp.Role.ToString());
                if (emp.Role == Role.Admin)
                    return RedirectToAction("Index", "Employee");
                else
                    return RedirectToAction("Profile", "Employee");
            }
            else
            {
                ViewData["ErrorMessage"] = "Email or password is invalid.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have successfully logged out.";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}