using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class Instructor : Controller
    {
        LconsultDBContext _context;
        public Instructor(LconsultDBContext context) {
            _context = context;

        }

        public IActionResult test()
        {
           string provider= _context.Courses.FirstOrDefault().Platform?.ToString()??"NA";
            return Content(provider);
        }

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
