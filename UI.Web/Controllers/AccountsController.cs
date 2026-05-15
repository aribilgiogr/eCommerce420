using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UI.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAuthService service;

        public AccountsController(IAuthService service)
        {
            this.service = service;
        }

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
        public async Task<IActionResult> Login(LoginDto model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var reply = await service.LoginAsync(model);
                if (reply.IsSuccess)
                {
                    return Redirect(returnUrl ?? "/");
                }
                else
                {
                    foreach (var error in reply.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }
            return View(model);
        }

        public IActionResult Register(string? returnUrl)
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var reply = await service.RegisterAsync(model);
                if (reply.IsSuccess)
                {
                    return Redirect(returnUrl ?? "/");
                }
                else
                {
                    foreach (var error in reply.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }
            return View(model);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Logout(string? returnUrl)
        {
            await service.LogoutAsync();
            return Redirect(returnUrl ?? "/");
        }
    }
}
