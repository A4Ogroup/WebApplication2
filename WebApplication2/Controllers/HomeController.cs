using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LconsultDBContext _context;

        public HomeController(ILogger<HomeController> logger,LconsultDBContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses.OrderByDescending(c => c.AverageRating)
                .Take(10).ToList();
         
            //var categoryList = _context.Categories.Select(C => new
            //{
            //    C.CategoryId,
            //    C.CategoryName
            //}).ToList();
            //ViewBag.Categories = categoryList;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Category()
        {
            return View();
        }
        public IActionResult Course()
        {
            return View();
        }
        public IActionResult New()
        {

            var courses = _context.Courses.OrderByDescending(c => c.AverageRating)
             .Take(10).ToList();
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult ForInstructor()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}