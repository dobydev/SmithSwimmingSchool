using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmithSwimmingSchool.Models;
using SmithSwimmingSchool.ViewModels;

namespace SmithSwimmingSchool.Controllers
{
    public class CoachController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public CoachController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        // Associate Coaches with their sessions
        public async Task<IActionResult> AllSession()
        {
            var session = await db.Sessions.Include(c => c.Coach).ToListAsync();
            return View(session);
        }

        // Coach profile (GET)
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user is null) return Challenge();

            var coach = await db.Coaches
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

            if (coach is null)
            {
                return View(new CoachProfileViewModel
                {
                    CoachName = user.Email ?? "Coach"
                });
            }

            return View(new CoachProfileViewModel
            {
                CoachName = coach.CoachName,
                PhoneNumber = coach.PhoneNumber,
                Bio = coach.Bio,
                Certifications = coach.Certifications
            });
        }


        // Coach profile (POST)
        [Authorize(Roles = "Coach, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(CoachProfileViewModel vm)
        {
            var user = await userManager.GetUserAsync(User);
            if (user is null) return Challenge();

            if (!ModelState.IsValid) return View(vm);

            var coach = await db.Coaches.FirstOrDefaultAsync(c => c.ApplicationUserId == user.Id);

            if (coach is null)
            {
                coach = new Coach
                {
                    ApplicationUserId = user.Id,
                    CoachName = vm.CoachName,
                    PhoneNumber = vm.PhoneNumber,
                    Bio = vm.Bio,
                    Certifications = vm.Certifications
                };
                await db.Coaches.AddAsync(coach);
            }
            else
            {
                coach.CoachName = vm.CoachName;
                coach.PhoneNumber = vm.PhoneNumber;
                coach.Bio = vm.Bio;
                coach.Certifications = vm.Certifications;

                db.Coaches.Update(coach);
            }

            await db.SaveChangesAsync();
            TempData["ProfileSaved"] = "Your profile was saved.";
            return RedirectToAction(nameof(Profile));
        }

        // Public views
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = await db.Coaches
                .Where(c => c.CoachId == id)
                .Select(c => new CoachProfileViewModel
                {
                    CoachName = c.CoachName,
                    PhoneNumber = c.PhoneNumber,
                    Certifications = c.Certifications,
                    Bio = c.Bio
                })
                .FirstOrDefaultAsync();

            if (model is null) return NotFound();

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var coaches = await db.Coaches.AsNoTracking()
                .OrderBy(c => c.CoachName)
                .ToListAsync();
            return View(coaches);
        }
    }
}

