using Microsoft.AspNetCore.Mvc;

namespace first.Controllers
{
    public class Instructor : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyCourses()
        {
            return View();
        }        
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult AddCourse()
        {
            return View();
        }
    }
}
