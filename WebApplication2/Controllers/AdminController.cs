using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using static WebApplication2.ViewModels.PagenationViewModel;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {

        LconsultDBContext _context;
        ICourseRepository _courseRepository;
        IReviewRepository _reviewRepository;
        IReportRepository _reportRepository;
        public AdminController(LconsultDBContext context,ICourseRepository courseRepository,IReviewRepository reviewRepository,IReportRepository reportRepository) {
            
            _context = context;
            _courseRepository = courseRepository; 
            _reviewRepository = reviewRepository;
            _reportRepository = reportRepository;

        }

        public ActionResult Index()
        {
            var course = _courseRepository.GetAll();
            var review = _reviewRepository.GetAll();
            var report = _reportRepository.GetAll();
            return View();
        }
        //public async Task<IActionResult> Index(int? pageNumber)
        //{
        //    int pageSize = 12;
        //    var reviews = _context.Reviews.Include(r => r.Course);
        //    return View(await PaginatedList<Review>.CreateAsync(reviews.AsNoTracking(), pageNumber ?? 1, pageSize));
        //}

        public async Task<IActionResult> Course(int? pageNumber)
        {
            int pageSize = 12;
            var course = _context.Courses.Include(r => r.Language);
            return View(await PaginatedList<Course>.CreateAsync(course.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        //public IActionResult Course()
        //{
        //    var courses = _courseRepository.GetAll().ToList();
        //    return View(courses);
        //}


        public async Task<IActionResult> Review(int? pageNumber)
        {
            int pageSize = 12;
            var reviews = _context.Reviews.Include(r => r.Course);
            return View(await PaginatedList<Review>.CreateAsync(reviews.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        
        public async Task<IActionResult> Report(int? pageNumber)
        {
            int pageSize = 12;
            var report = _context.Reports.Include(r => r.Review);
            return View(await PaginatedList<Report>.CreateAsync(report.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        //public IActionResult Review()
        //{
        //    var review = _reviewRepository.GetAllWithCourse().ToList();
        //    return View(review);
        //}

    }
}
