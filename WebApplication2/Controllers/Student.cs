using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class Student : Controller
    {
        private readonly LconsultDBContext _context;

        public Student (LconsultDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            var courses = _context.Courses.OrderByDescending(c => c.AverageRating)
            .Take(3).ToList();

            var reviews = _context.Reviews.OrderByDescending(c => c.Rate)
                    .Take(3).ToList();
            var latestCourses = _context.Courses.OrderByDescending(c => c.AddingDate)
                .Take(3).ToList();

            return View();
        }

        public IActionResult Profile()
        {
            //itsme
            return View();
        }
        public IActionResult Review()
        {
            return View();
        }




    }
}
