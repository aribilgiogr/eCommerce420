using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using Microsoft.AspNetCore.Identity;
using Tools.Responses;

namespace Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public async Task<Reply> LoginAsync(LoginDto model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return Reply.Success();
            }
            else if (result.IsLockedOut)
            {
                return Reply.Fail("User is locked out, try again later.");
            }
            else if (result.IsNotAllowed)
            {
                return Reply.Fail("Login attempt failed!");
            }
            else if (result.RequiresTwoFactor)
            {
                return Reply.Fail("We need TwoFactor validation!");
            }
            else
            {
                return Reply.Fail("Email address of Password not valid!");
            }
        }

        public async Task LogoutAsync() => await signInManager.SignOutAsync();

        public async Task<Reply> RegisterAsync(RegisterDto model)
        {
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!roleManager.Roles.Any(x => x.Name == "Customer"))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "Customer" });
                }

                if (!roleManager.Roles.Any(x => x.Name == "Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                }

                await userManager.AddToRoleAsync(user, "Customer");

                var customer = new Customer { AccountId = user.Id, Firstname = string.Empty, Lastname = string.Empty, ProfilePicture = string.Empty };

                await unitOfWork.Customers.CreateAsync(customer);

                return await unitOfWork.CommitAsync();
            }
            else
            {
                return Reply.Fail(result.Errors.Select(e => e.Description));
            }
        }
    }
}
