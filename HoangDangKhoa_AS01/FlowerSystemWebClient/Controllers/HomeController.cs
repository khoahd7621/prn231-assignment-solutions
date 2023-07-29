using BusinessObjects;
using DataTransfer;
using FlowerSystemWebClient.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace FlowerSystemWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client = null;
        private string CustomerApiUrl = "";

        public HomeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CustomerApiUrl = "http://localhost:5274/api/Customer";
        }

        [HttpGet]
        public IActionResult Index()
        {
            string Role = HttpContext.Session.GetString("ROLE");
            if (Role == "Admin")
                return RedirectToAction("Index", "Customer");
            else if (Role == "Customer")
                return RedirectToAction("Profile", "Customer");

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
            HttpResponseMessage response = await client.GetAsync(CustomerApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();

            Customer admin = new Customer
            {
                CustomerName = "Admin",
                Email = config["Credentials:Email"],
                Password = config["Credentials:Password"],
                Role = "Admin"
            };

            List<Customer> listCustomers = JsonSerializer.Deserialize<List<Customer>>(stringData, options);
            listCustomers.Add(admin);
            Customer account = listCustomers.Where(c => c.Email == loginRequest.Email && c.Password == loginRequest.Password).FirstOrDefault();

            if (account != null)
            {
                HttpContext.Session.SetInt32("USERID", account.CustomerID);
                HttpContext.Session.SetString("USERNAME", account.CustomerName);
                HttpContext.Session.SetString("ROLE", account.Role);
                if (account.Role == "Admin")
                    return RedirectToAction("Index", "Customer");
                else
                    return RedirectToAction("Profile", "Customer");
            }
            else
            {
                ViewData["ErrorMessage"] = "Email or password is invalid.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            string Role = HttpContext.Session.GetString("ROLE");
            if (Role == "Admin")
                return RedirectToAction("Index", "Customer");
            else if (Role == "Customer")
                return RedirectToAction("Profile", "Customer");

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerRequest customerRequest)
        {
            Customer customer = await ApiHandler.DeserializeApiResponse<Customer>(CustomerApiUrl + "/Email/" + customerRequest.Email, HttpMethod.Get);
            if (customerRequest.Email.Equals("admin@FUflowerbouquet.com") ||
                (customer != null && customer.CustomerID != 0))
            {
                ViewData["ErrorMessage"] = "Email already exists.";
                return View("Register");
            }

            await ApiHandler.DeserializeApiResponse(CustomerApiUrl, HttpMethod.Post, customerRequest);

            ViewData["SuccessMessage"] = "Register new account successfully.";
            return View("Index");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}