using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmithSwimmingSchool.Models;
namespace SmithSwimmingSchool.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext db;
    public AdminController(ApplicationDbContext context) => db = context;

   
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddLesson()
    {
        return View();
    }

    // POST: /Admin/AddLesson
    [HttpPost]
    public async Task<IActionResult> AddLesson(Lesson model)
    {
        if (!ModelState.IsValid)
        {
            // Repopulate dropdowns if validation fails
            ViewBag.CoachList = new SelectList(await db.Coaches
                .Select(c => new { c.CoachName })
                .ToListAsync(), "CoachName");

            ViewBag.SwimmerList = new SelectList(await db.Swimmers
                .Select(s => new { s.SwimmerId, s.SwimmerName })
                .ToListAsync(), "SwimmerName");

            return View(model);
        }

        db.Lessons.Add(model);
        await db.SaveChangesAsync();

        // Redirect back to the AllLessons page after saving
        return RedirectToAction(nameof(AllLessons));
    }

    // LIST
    [HttpGet]
    public async Task<IActionResult> AllLessons()
    {
        var lessons = await db.Lessons
            .Include(l => l.Coach)
            .Include(l => l.Swimmer)
            .ToListAsync();

        return View(lessons); // Views/Admin/AllLessons.cshtml
    }

    // GET: /Admin/EditLesson/5    <-- this is what your link calls
    [HttpGet]
    public async Task<IActionResult> EditLesson(int id)
    {
        var lesson = await db.Lessons
            .Include(l => l.Coach)
            .Include(l => l.Swimmer)
            .FirstOrDefaultAsync(l => l.LessonId == id);

        if (lesson == null) return NotFound();

        await PopulateDropDowns(lesson.CoachId, lesson.SwimmerId);
        return View(lesson); // Views/Admin/EditLesson.cshtml must exist
    }

    // POST: /Admin/EditLesson
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditLesson(Lesson model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropDowns(model.CoachId, model.SwimmerId);
            return View(model);
        }

        db.Attach(model);
        db.Entry(model).Property(x => x.SkillLevel).IsModified = true;
        db.Entry(model).Property(x => x.Tuition).IsModified = true;
        db.Entry(model).Property(x => x.CoachId).IsModified = true;
        db.Entry(model).Property(x => x.SwimmerId).IsModified = true;

        await db.SaveChangesAsync();
        return RedirectToAction(nameof(AllLessons));
    }

    // GET: /Admin/DeleteLesson/5
    [HttpGet]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        var lesson = await db.Lessons
            .Include(l => l.Sessions)
            .Include(l => l.Coach)
            .Include(l => l.Swimmer)
            .FirstOrDefaultAsync(l => l.LessonId == id);

        if (lesson == null) return NotFound();
        return View(lesson); // Views/Admin/DeleteLesson.cshtml
    }

    // POST: /Admin/DeleteLesson
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteLessonAsync(int lessonId)
    {
        var lesson = await db.Lessons
            .Include(l => l.Sessions)
            .FirstOrDefaultAsync(l => l.LessonId == lessonId);

        if (lesson == null) return NotFound();
        if (lesson.Sessions != null && lesson.Sessions.Any())
        {
            // safety net — don’t delete if sessions exist
            TempData["Error"] = "Cannot delete a lesson with scheduled sessions.";
            return RedirectToAction(nameof(DeleteLesson), new { id = lessonId });
        }

        db.Lessons.Remove(lesson);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(AllLessons));
    }

    private async Task PopulateDropDowns(int? coachId = null, int? swimmerId = null)
    {
        var coaches = await db.Coaches
            .Select(c => new { c.CoachId, c.CoachName })
            .ToListAsync();
        var swimmers = await db.Swimmers
            .Select(s => new { s.SwimmerId, s.SwimmerName })
            .ToListAsync();

        ViewBag.CoachList = new SelectList(coaches, "CoachId", "CoachName", coachId);
        ViewBag.SwimmerList = new SelectList(swimmers, "SwimmerId", "SwimmerName", swimmerId);
    }
}
