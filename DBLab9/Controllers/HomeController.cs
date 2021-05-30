using Microsoft.AspNetCore.Mvc;

namespace DBLab9.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}