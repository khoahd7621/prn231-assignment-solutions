using BusinessObjects;
using FlowerSystemWebClient.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace FlowerSystemWebClient.Controllers
{
    public class FlowerBouquetController : Controller
    {
        private readonly HttpClient client = null;
        private string FlowerBouquetApiUrl = "";
        private string CategoryApiUrl = "";
        private string SupplierApiUrl = "";

        public FlowerBouquetController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            FlowerBouquetApiUrl = "http://localhost:5274/api/FlowerBouquet";
            CategoryApiUrl = "http://localhost:5274/api/Category";
            SupplierApiUrl = "http://localhost:5274/api/Supplier";
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

            List<FlowerBouquet> listFlowerBouquets = await ApiHandler.DeserializeApiResponse<List<FlowerBouquet>>(FlowerBouquetApiUrl, HttpMethod.Get);

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(listFlowerBouquets);
        }

        public async Task<IActionResult> Search(string keyword)
        {
            List<FlowerBouquet> listCustomers = await ApiHandler.DeserializeApiResponse<List<FlowerBouquet>>(FlowerBouquetApiUrl + "/Search/" + keyword, HttpMethod.Get);

            ViewData["keyword"] = keyword;

            return View("Index", listCustomers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
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

            List<Category> listCategories = await ApiHandler.DeserializeApiResponse<List<Category>>(CategoryApiUrl, HttpMethod.Get);
            List<Supplier> listSuppliers = await ApiHandler.DeserializeApiResponse<List<Supplier>>(SupplierApiUrl, HttpMethod.Get);

            ViewData["Categories"] = listCategories;
            ViewData["Suppliers"] = listSuppliers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FlowerBouquet flowerBouquet)
        {
            flowerBouquet.FlowerBouquetStatus = 1;
            HttpResponseMessage response = await client.PostAsJsonAsync(FlowerBouquetApiUrl, flowerBouquet);
            response.EnsureSuccessStatusCode();
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

            FlowerBouquet flowerBouquet = await ApiHandler.DeserializeApiResponse<FlowerBouquet>(FlowerBouquetApiUrl + "/" + id, HttpMethod.Get);
            if (flowerBouquet == null)
            {
                TempData["ErrorMessage"] = "Flower Bouquet not found";
                return RedirectToAction("Index");
            }
            if (flowerBouquet.FlowerBouquetStatus == 0)
            {
                TempData["ErrorMessage"] = "Flower Bouquet is not available";
                return RedirectToAction("Index");
            }
            
            List<Category> listCategories = await ApiHandler.DeserializeApiResponse<List<Category>>(CategoryApiUrl, HttpMethod.Get);
            List<Supplier> listSuppliers = await ApiHandler.DeserializeApiResponse<List<Supplier>>(SupplierApiUrl, HttpMethod.Get);

            ViewData["Categories"] = listCategories;
            ViewData["Suppliers"] = listSuppliers;

            if (TempData != null)
            {
                ViewData["SuccessMessage"] = TempData["SuccessMessage"];
                ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            }

            return View(flowerBouquet);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FlowerBouquet flowerBouquet)
        {
            flowerBouquet.FlowerBouquetStatus = 1;
            await ApiHandler.DeserializeApiResponse(FlowerBouquetApiUrl + "/" + flowerBouquet.FlowerBouquetID, HttpMethod.Put, flowerBouquet);
            TempData["SuccessMessage"] = "Flower Bouquet updated successfully";
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

            FlowerBouquet flowerBouquet = await ApiHandler.DeserializeApiResponse<FlowerBouquet>(FlowerBouquetApiUrl + "/" + id, HttpMethod.Get);
            if (flowerBouquet == null)
            {
                TempData["ErrorMessage"] = "Flower Bouquet not found";
                return RedirectToAction("Index");
            }
            if (flowerBouquet.FlowerBouquetStatus == 0)
            {
                TempData["ErrorMessage"] = "Flower Bouquet is not available";
                return RedirectToAction("Index");
            }

            return View(flowerBouquet);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(FlowerBouquet flowerBouquet)
        {
            await ApiHandler.DeserializeApiResponse<FlowerBouquet>(FlowerBouquetApiUrl + "/" + flowerBouquet.FlowerBouquetID, HttpMethod.Delete);
            TempData["SuccessMessage"] = "Flower Bouquet deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
