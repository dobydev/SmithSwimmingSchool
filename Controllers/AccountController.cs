using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmithSwimmingSchool.Models;
using SmithSwimmingSchool.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;

namespace SmithSwimmingSchool.Controllers
{
    public class AccountController : Controller
    {
        // Dependency Injection
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<IdentityRole> roleManager;

        // UserManager, SignInManager, and RoleManager are part of ASP.NET Core Identity
        public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager, ApplicationDbContext db)

        {
            // Assign injected services to private fields
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.db = db;
        }

        // Action Method to show the registration form
        public IActionResult Register()

        {
            return View();
        }

        // Action Method to handle registration form submission
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)

        {
            // Check if the model state is valid
            if (ModelState.IsValid)

            {
                // Create a new ApplicationUser instance
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Email = vm.Email
                };

                // Create the user with the specified password
                var result = await userManager
                    .CreateAsync(user, vm.Password);

                // If user creation is successful, sign in the user
                if (result.Succeeded)
                {
                    // Sign in the user
                    await signInManager.SignInAsync(user, false);

                    // Redirect to Login Page
                    return RedirectToAction("Login", "Account");
                }

                // If there are errors, add them to the model state
                else
                {
                    // Add errors to the model state
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }

        // Action Method to show the login form
        public IActionResult Login()

        {
            return View();
        }

        // Action Method to handle login form submission
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Attempt to sign in the user
                var result = await signInManager.PasswordSignInAsync(vm.Email, vm.Password, false, false);

                // If sign-in is successful, redirect to the appropriate page based on user role
                if (result.Succeeded)

                {
                    // Retrieve the user and their roles
                    var user = await userManager.FindByEmailAsync(vm.Email);
                    var roles = await userManager.GetRolesAsync(user!);

                    // Redirect based on the user's roles
                    if (roles.Count > 1)
                    {
                        HttpContext.Session.SetString("UserRoles", JsonConvert.SerializeObject(roles));
                        return RedirectToAction("SelectRole");
                    }

                    // If the user is an Admin, redirect to the Admin controller
                    else if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    // If the user is a Coach, redirect to the Coach controller
                    else if (roles.Contains("Coach"))
                    {
                        return RedirectToAction("Index", "Coach");
                    }

                    // If the user is a Swimmer, redirect to the Swimmer controller
                    else if (roles.Contains("Swimmer"))
                    {
                        return RedirectToAction("Index", "Swimmer");
                    }

                    // Default redirect if no roles match
                    return RedirectToAction("Index", "Home");

                }
                // If sign-in fails, add an error to the model state
                ModelState.AddModelError("", "Login Failure.");
            }
            return View(vm);
        }

        public IActionResult AllUser()

        {
            // Retrieve all users from the database
            var users = db.Users.ToList();
            var userRoles = new Dictionary<string, List<string>>();

            // Retrieve roles for each user
            foreach (var user in users)
            {
                var roles = userManager.GetRolesAsync(user).Result.ToList();
                userRoles[user.Id] = roles;
            }

            // Pass the user roles to the view using ViewBag
            ViewBag.UserRoles = userRoles;

            return View(users);
        }


        // Action Method to select role if multiple roles exist
        public IActionResult SelectRole()
        {
            // Retrieve the roles from the session
            var roles = HttpContext.Session.GetString("UserRoles");

            // Check for null before deserializing
            if (string.IsNullOrEmpty(roles))
            {
                // Handle the case where roles are not found in session
                return RedirectToAction("Login", "Account");
            }

            // Deserialize the roles JSON string into a list of strings
            var roleList = JsonConvert.DeserializeObject<List<string>>(roles);

            return View(roleList);
        }

        // Action Method to set the selected role and redirect accordingly
        [HttpPost]
        public IActionResult SetRole(string SelectedRole)
        {
            // If the selected role is "Admin", redirect to the Admin controller
            if (SelectedRole == "Admin")
            {
                // Redirect to Admin controller
                return RedirectToAction("Index", "Admin");
            }

            // If the selected role is "Coach", redirect to the Coach controller
            else if (SelectedRole == "Coach")
            {
                return RedirectToAction("Index", "Coach");
            }

            // If the selected role is "Swimmer", redirect to the Swimmer controller
            else if (SelectedRole == "Swimmer")
            {
                return RedirectToAction("Index", "Swimmer");
            }

            // Default redirect if no roles match
            return RedirectToAction("Index", "Home");

        }

        // Action Method to handle user logout
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Clear app session data (if any)
            HttpContext.Session.Clear();

            // Sign out identity cookies
            await signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Redirect to a safe page
            return RedirectToAction("Index", "Home");
        }

    }
}
