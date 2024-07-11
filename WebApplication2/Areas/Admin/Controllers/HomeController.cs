using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Repository;


namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        LconsultDBContext _context;
        ICourseRepository _courseRepository;
        IReviewRepository _reviewRepository;
        IReportRepository _reportRepository;
        public HomeController(LconsultDBContext context, ICourseRepository courseRepository, IReviewRepository reviewRepository, IReportRepository reportRepository)
        {

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


        public IActionResult ToggleStatus(int id)
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
            return RedirectToAction("course", "admin"); // Assuming Index is your admin page
        }

        public IActionResult ReviewToggleStatus(int id)
        {
            var review = _reviewRepository.GetById(id);
            if (review == null)
            {
                return NotFound();
            }

            review.Status = !review.Status; // Toggle the boolean status
            _reviewRepository.Update(review);
            _reviewRepository.Save();
            TempData["Success"] = "Review status changed successfully!";
            return RedirectToAction("review", "admin"); // Assuming Index is your admin page
        }

        public IActionResult ReportToggleStatus(int id)
        {
            var report = _reportRepository.GetById(id);
            if (report == null)
            {
                return NotFound();
            }

            report.Status = !report.Status; // Toggle the boolean status
            _reportRepository.Update(report);
            _reportRepository.Save();
            TempData["Success"] = "Report status changed successfully!";
            return RedirectToAction("report", "admin"); // Assuming Index is your admin page
        }


    }

}

