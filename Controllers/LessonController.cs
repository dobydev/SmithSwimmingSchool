using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmithSwimmingSchool.Models;

namespace SmithSwimmingSchool.Controllers
{
    public class LessonController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public LessonController(ApplicationDbContext ctx) => _ctx = ctx;


        // Static list to hold all lessons
        static List<Lesson> AllLessons = new List<Lesson>();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            // Load the lesson with its sessions
            var lesson = await _ctx.Lessons
                .Include(l => l.Sessions)
                .FirstOrDefaultAsync(l => l.LessonId == id);

            // If lesson not found, return 404
            if (lesson == null) return NotFound();
            return View("DeleteLesson", lesson);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLessonConfirmed(int lessonId)
        {
            // Load the lesson with its sessions
            var lesson = await _ctx.Lessons
                .Include(l => l.Sessions)
                .FirstOrDefaultAsync(l => l.LessonId == lessonId);

            // If lesson not found, return 404
            if (lesson == null) return NotFound();

            // Check if there are any sessions associated with the lesson
            if (lesson.Sessions.Any())
            {
                TempData["DeleteError"] = "Cannot delete: sessions are scheduled.";
                return RedirectToAction(nameof(DeleteLesson), new { id = lessonId });
            }

            // Remove the lesson and save changes
            _ctx.Lessons.Remove(lesson);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("AllLessons");
        }
    }
}
