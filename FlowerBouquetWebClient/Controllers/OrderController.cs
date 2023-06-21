using FlowerBouquetWebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerBouquetWebClient.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
