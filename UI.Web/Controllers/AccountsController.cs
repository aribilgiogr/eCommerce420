using Core.Concretes.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UI.Web.Controllers
{
    public class AccountsController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string? returnUrl)
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(LoginDto model, string? returnUrl)
        {
            return View();
        }

        public IActionResult Register(string? returnUrl)
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Register(RegisterDto model, string? returnUrl)
        {
            return View();
        }

        [HttpPost, Authorize]
        public IActionResult Logout(string? returnUrl)
        {
            return Redirect(returnUrl ?? "/");
        }
    }
}
