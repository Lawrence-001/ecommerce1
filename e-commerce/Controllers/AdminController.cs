using e_commerce.Data;
using e_commerce.Models;
using e_commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace e_commerce.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRole(RoleVM roleVM)
        {
            IdentityRole role = new IdentityRole()
            {
                Name = roleVM.RoleName
            };
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("listroles", "admin");
            }
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(roleVM);

        }
        public async Task<IActionResult> AddUserToRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityRole role = await _roleManager.FindByNameAsync(roleName);
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ListRoles()
        {
            var rolesList = await _roleManager.Roles.ToListAsync();
            return View(rolesList);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                EditRoleVM editRoleVM = new EditRoleVM()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                return View(editRoleVM);
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> EditRole(EditRoleVM model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                role.Name = model.RoleName;
                IdentityResult result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("listroles");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                
            }
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role!=null)
            {
                await _roleManager.DeleteAsync(role);
                return RedirectToAction("listroles");
            }
            return View("NotFound");
        }
    }
}
