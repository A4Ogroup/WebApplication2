using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Repository;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeeController : Controller
    {
        LconsultDBContext _context;
        ICourseRepository _courseRepository;
        IReviewRepository _reviewRepository;
        IReportRepository _reportRepository;
        public HomeeController(LconsultDBContext context, ICourseRepository courseRepository, IReviewRepository reviewRepository, IReportRepository reportRepository)
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
        public async Task<IActionResult> Course(int? pageNumber)
        {
            int pageSize = 12;
            var course = _courseRepository.GetAllWithLanguage().AsQueryable();
            return View(await PaginatedListNew<Course>.CreateAsync(course.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        
        public async Task<IActionResult> CourseReports(int? pageNumber)
        {
            int pageSize = 12;
            var course = _courseRepository.GetAllWithLanguage().AsQueryable();
            return View(await PaginatedListNew<Course>.CreateAsync(course.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> FilterCourses(string filterType, int? pageNumber, bool isDescending = false)
        {
            //IQueryable<Course> courses = _courseRepository.GetAll().AsQueryable();

            int pageSize = 12;
            var course = _courseRepository.GetAll().AsQueryable();
            switch (filterType)
            {
                case "Title":

                    course = course.OrderByDescending(c => c.Title);
                    
                    break;
                case "Date":
                   // courses = isDescending ? course.OrderByDescending(c => c.AddingDate).ToList() : course.OrderBy(c => c.AddingDate).ToList();
                    course = course.OrderByDescending(c => c.AddingDate);
                    break;
                case "AddedToday":
                    var today = DateTime.Today;
                    course = course.Where(c => c.AddingDate.Date == today);
                    break;
                case "LastMonth":
                    // Calculate the date range for the last month
                    today = DateTime.Today;
                    var firstDayOfLastMonth = new DateTime(today.Year, today.Month - 1, 1);
                    var lastDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
                    course = course.Where(c => c.AddingDate>= firstDayOfLastMonth && c.AddingDate <=lastDayOfLastMonth);
                    break;
                default:
                  
                    break;
            }
           // var paginatedCourses = await PaginatedListNew<Course>.CreateAsync(courses, pageIndex, PageSize);
            return View("CourseReports", await PaginatedListNew<Course>.CreateAsync(course.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

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
            return RedirectToAction("course", "homee"); // Assuming Index is your admin page
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
            return RedirectToAction("review", "homee"); // Assuming Index is your admin page
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
            return RedirectToAction("report", "homee"); // Assuming Index is your admin page
        }
    }
}
