using BusinessObjects;
using DataTransfer;
using FlowerSystemWebClient.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace FlowerSystemWebClient.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient client = null;
        private string CustomerApiUrl = "";

        public CustomerController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CustomerApiUrl = "http://localhost:5274/api/Customer";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string Role = HttpContext.Session.GetString("ROLE");

            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Customer");
            }

            List<Customer> listCustomers = await ApiHandler.DeserializeApiResponse<List<Customer>>(CustomerApiUrl, HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(listCustomers);
        }

        public async Task<IActionResult> Search(string keyword)
        {
            List<Customer> listCustomers = await ApiHandler.DeserializeApiResponse<List<Customer>>(CustomerApiUrl + "/Search/" + keyword, HttpMethod.Get);

            ViewData["keyword"] = keyword;

            return View("Index", listCustomers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            string Role = HttpContext.Session.GetString("ROLE");

            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Customer");
            }

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequest customerRequest)
        {
            Customer customer = await ApiHandler.DeserializeApiResponse<Customer>(CustomerApiUrl + "/Email/" + customerRequest.Email, HttpMethod.Get);
            if (customerRequest.Email.Equals("admin@FUflowerbouquet.com") ||
                (customer != null && customer.CustomerID != 0))
            {
                TempData["ErrorMessage"] = "Email already exists.";
                return RedirectToAction("Create");
            }

            await ApiHandler.DeserializeApiResponse(CustomerApiUrl, HttpMethod.Post, customerRequest);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string Role = HttpContext.Session.GetString("ROLE");

            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Customer");
            }

            Customer customer = await ApiHandler.DeserializeApiResponse<Customer>(CustomerApiUrl + "/" + id, HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerRequest customerRequest)
        {
            await ApiHandler.DeserializeApiResponse(CustomerApiUrl + "/" + customerRequest.CustomerID, HttpMethod.Put, customerRequest);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string Role = HttpContext.Session.GetString("ROLE");

            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Admin")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Profile", "Customer");
            }

            Customer customer = await ApiHandler.DeserializeApiResponse<Customer>(CustomerApiUrl + "/" + id, HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CustomerRequest customerRequest)
        {
            HttpResponseMessage response = await client.DeleteAsync(CustomerApiUrl + "/" + customerRequest.CustomerID);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return View();
        }

        // Customer
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            string Role = HttpContext.Session.GetString("ROLE");
            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Customer")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Index", "Customer");
            }
            int userId = HttpContext.Session.GetInt32("USERID").Value;

            Customer customer = await ApiHandler.DeserializeApiResponse<Customer>(CustomerApiUrl + "/" + userId, HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            string Role = HttpContext.Session.GetString("ROLE");
            if (Role == null)
            {
                TempData["ErrorMessage"] = "You must login to access this page.";
                return RedirectToAction("Index", "Home");
            }
            else if (Role != "Customer")
            {
                TempData["ErrorMessage"] = "You don't have permission to access this page.";
                return RedirectToAction("Index", "Customer");
            }
            int userId = HttpContext.Session.GetInt32("USERID").Value;

            Customer customer = await ApiHandler.DeserializeApiResponse<Customer>(CustomerApiUrl + "/" + userId, HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(CustomerRequest customerRequest)
        {
            int? userId = HttpContext.Session.GetInt32("USERID");
            if (userId == null)
                return RedirectToAction("Index", "Home");

            customerRequest.CustomerID = userId.Value;
            await ApiHandler.DeserializeApiResponse(CustomerApiUrl + "/" + customerRequest.CustomerID, HttpMethod.Put, customerRequest);

            TempData["SuccessMessage"] = "Edit profile information successfully.";

            return RedirectToAction("Profile", TempData);
        }
    }
}
