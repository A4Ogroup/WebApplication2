using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class Instructor : Controller
    {
       private readonly LconsultDBContext _context;
       private readonly ICourseRepository _courseRepository;
        public Instructor(LconsultDBContext context,ICourseRepository Course) {
            _context = context;
            _courseRepository = Course;
        }

        public async Task<IActionResult> GetSubcategories(int categoryId)
        {
            var subcategories = await _context.SubCategories
                .Where(s => s.CategoryId == categoryId)
                .Select(s => new { s.SubId, s.SubName })
                .ToListAsync();

            return Json(subcategories);
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
            ViewBag.Categories= _context.Categories;
            ViewBag.Languages= _context.Languages;
            return View();
        }



        [HttpPost]
        public IActionResult AddCourse(AddCourseViewModel course)
        {
            
            return View();
        }
    }
}
