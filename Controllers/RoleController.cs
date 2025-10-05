using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmithSwimmingSchool.Models;
using SmithSwimmingSchool.ViewModels;

namespace SmithSwimmingSchool.Controllers
{
    // Uncomment this when you want to restrict access to Admins only
    // Ensure you assign your account as an admin
    // [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        // Properties
        ApplicationDbContext db;
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> roleManager;

        // Constructor
        public RoleController(ApplicationDbContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            // Dependency Injection
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllRole()

        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult AddRole()

        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(IdentityRole role)

        {
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("AllRole");
            }

            return View();

        }

        public async Task<IActionResult> AddUserRole(string id)

        {
            var roleDisplay = db.Roles.Select(x => new { Id = x.Id, Value = x.Name }).ToList();

            RoleAddUserRoleViewModel vm = new RoleAddUserRoleViewModel();

            var user = await userManager.FindByIdAsync(id);
            vm.User = user;
            vm.RoleList = new SelectList(roleDisplay, "Id", "Value");

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> AddUserRole(RoleAddUserRoleViewModel vm)

        {
            // Find the user and role
            var user = await userManager.FindByIdAsync(vm.User.Id);
            // Find the role by Id
            var role = await roleManager.FindByIdAsync(vm.Role);
            // Add the user to the role
            var result = await userManager.AddToRoleAsync(user, role.Name);
            // If successful, redirect to AllUser action in Account controller
            if (result.Succeeded)
            {
                return RedirectToAction("AllUser", "Account");
            }
            // If there are errors, add them to the ModelState
            foreach (var error in result.Errors)
            {

                ModelState.AddModelError(error.Code, error.Description);

            }

            // Repopulate the role list for the view model
            var roleDisplay = db.Roles.Select(x => new {
                Id = x.Id,

                Value = x.Name
            }).ToList();

            //  Reassign the user to the view model
            vm.User = user;
            vm.RoleList = new SelectList(roleDisplay, "Id", "Value");

            return View(vm);

        }

    }
}
