using Microsoft.AspNetCore.Mvc;

namespace SmithSwimmingSchool.Controllers
{
    public class Visitor : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
