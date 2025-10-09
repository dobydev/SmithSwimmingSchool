using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmithSwimmingSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace SmithSwimmingSchool.Controllers
{

    public class CoachController : Controller
    {
        ApplicationDbContext db;
        public CoachController(ApplicationDbContext db)
    {
        this.db = db;
    }

        [Authorize]
       
        // Associate Coaches with their sessions
        public async Task<IActionResult> AllSession()
        {
            var session = await db.Sessions.Include(c => c.Coach).ToListAsync();
            return View(session);
        }
    }

}
