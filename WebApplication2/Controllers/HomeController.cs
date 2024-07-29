using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseRepository _courseRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly LconsultDBContext _context;

        public HomeController(ILogger<HomeController> logger,LconsultDBContext context,ICourseRepository courseRepository,IReviewRepository reviewRepository)
        {
            _context = context;
            _logger = logger;
            _courseRepository = courseRepository;
            _reviewRepository = reviewRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var _topCourses = _courseRepository.GetTopCourses();
            var _topReviews = _reviewRepository.GetTopReviews();

            IndexViewModel top = new IndexViewModel
            {
                Course = _topCourses,
                Review = _topReviews
            };
            return View(top);
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
        [HttpGet]
        public IActionResult New()
        {

            return View();
        }
        [HttpPost]
        public IActionResult New(InstructorRegisterViewModel model)
        {
            User usermode = new User();
            usermode.PhoneNumber = model.PhoneNumber;

            return View();
        }

        public IActionResult Test()
        {
            var courses = _context.Courses.Select(c => new
            {
                c.Title,
                c.CourseDescription,
                c.CourseDuration,
                c.Subcategory,
                c.TopicsCovered,
                c.InstructorFullName,
                c.Language,
                c.Level,
                c.Picture,
                c.Platform,
                c.Link,
                c.PriceStatus,
                c.AverageRating,

            }).ToList();

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
            return RedirectToAction("Register","Account");
        }

        [AllowAnonymous]
        [Route("Home/StatusCode")]
        public IActionResult StatusCode(int code)
        {
            if (code == 404)
            {
               return View("NotFound");
            }

            return View("Error");
        }
        [AllowAnonymous]

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}