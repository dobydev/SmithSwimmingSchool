using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmithSwimmingSchool.Controllers
{
    public class CoachController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
