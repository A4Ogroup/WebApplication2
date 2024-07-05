using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var course = _courseRepository.GetAllWithLanguage().AsQueryable();
            return View(await PaginatedListNew<Course>.CreateAsync(course.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        //public IActionResult Course()
        //{
        //    var courses = _courseRepository.GetAll().ToList();
        //    return View(courses);
        //}


        public async Task<IActionResult> Review(int? pageNumber)
        {
            int pageSize = 12;
            var reviews = _reviewRepository.GetAllWithCourse().AsQueryable();
            return View(await PaginatedListNew<Review>.CreateAsync(reviews.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        
        public async Task<IActionResult> Report(int? pageNumber)
        {
            int pageSize = 12;
            var report = _reportRepository.GetAllWithReview().AsQueryable();
            return View(await PaginatedListNew<Report>.CreateAsync(report.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        //public IActionResult Review()
        //{
        //    var review = _reviewRepository.GetAllWithCourse().ToList();
        //    return View(review);
        //}


        public  IActionResult ToggleStatus(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            course.Status = !course.Status; // Toggle the boolean status
            _courseRepository.Update(course);
             _courseRepository.Save();
            TempData["Success"] = "Course status changed successfully!";
            return RedirectToAction("course","admin"); // Assuming Index is your admin page
        }


    }

}

