using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.ViewModels;

namespace  WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {

                IdentityRole role = new IdentityRole();
                role.Name = roleModel.RoleName;

                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Role added successfully!";
                    return RedirectToAction("Index", "homee");
                }
                else
                {
                    foreach (var items in result.Errors)
                    {
                        ModelState.AddModelError("", items.Description);
                    }
                }

            }
            return View(roleModel);
        }
    }
}
