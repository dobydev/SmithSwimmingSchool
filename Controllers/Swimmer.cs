using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmithSwimmingSchool.Controllers
{
    public class Swimmer : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
