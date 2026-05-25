using Core.Abstracts.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UI.Web.Models;

namespace UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShopService shopService;
        
        public HomeController(ILogger<HomeController> logger, IShopService shopService)
        {
            _logger = logger;
            this.shopService = shopService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await shopService.GetProducts();
            return View(products);
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

        [HttpPost, Authorize]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await shopService.AddToCart(userId, productId, quantity);
            return RedirectToAction("Index");
        }
    }
}
