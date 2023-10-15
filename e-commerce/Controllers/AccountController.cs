using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Users()
        {
            var users = _userManager.Users;
            return View(users);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationVM model)
        {
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser()
                {
                    UserName=model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                    
                };
              //  newUser.UserName = model.Email;
                var result = await _userManager.CreateAsync(newUser, model.Password);
                
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Login");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                var attemptLogin = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (attemptLogin.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                    
                }
                ModelState.AddModelError(string.Empty, "Invalid credentials, please try again");
            }
            return View(model);

        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

       
    }
    
}
