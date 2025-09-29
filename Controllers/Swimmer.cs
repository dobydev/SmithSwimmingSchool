using Microsoft.AspNetCore.Mvc;

namespace SmithSwimmingSchool.Controllers
{
    public class Swimmer : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
