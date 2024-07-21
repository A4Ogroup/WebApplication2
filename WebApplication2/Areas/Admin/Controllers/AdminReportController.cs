using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Repository;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using WebApplication2.ViewModels;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminReportController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IStudentRepository _studentRepository;
        public AdminReportController(ICourseRepository courseRepository, IReviewRepository reviewRepository, IInstructorRepository instructorRepository, IReportRepository reportRepository, IStudentRepository studentRepository)
        {
            _courseRepository = courseRepository;
            _reviewRepository = reviewRepository;
            _instructorRepository = instructorRepository;
            _reportRepository = reportRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IActionResult> CourseReports(int? pageNumber)
        {
            int pageSize = 15;
            var course = _courseRepository.GetAllWithLanguage().AsQueryable();
            return View(await PaginatedListNew<Course>.CreateAsync(course.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> ReviewReports(int? pageNumber)
        {
            int pageSize = 15;
            var review = _reviewRepository.GetAllWithCourse().AsQueryable();
            return View(await PaginatedListNew<Review>.CreateAsync(review.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> InstructorReports(int? pageNumber)
        {
            int pageSize = 15;
            var instructor = _instructorRepository.GetAll().AsQueryable();
            return View(await PaginatedListNew<Instructor>.CreateAsync(instructor.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        public async Task<IActionResult> StudentReports(int? pageNumber)
        {
            int pageSize = 15;
            var student = _studentRepository.GetAll().AsQueryable();
            return View(await PaginatedListNew<Student>.CreateAsync(student.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> FilterCourses(string filterType, int? pageNumber)
        {
            //IQueryable<Course> courses = _courseRepository.GetAll().AsQueryable();

            int pageSize = 15;
            var course = _courseRepository.GetAll().AsQueryable();
            switch (filterType)
            {
                case "AddedToday":
                    var today = DateTime.Today;
                    course = course.Where(c => c.AddingDate.Date == today);
                    break;
                case "LastMonth":
                    // Calculate the date range for the last month
                    today = DateTime.Today;
                    var firstDayOfLastMonth = new DateTime(today.Year, today.Month - 1, 1);
                    var lastDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
                    course = course.Where(c => c.AddingDate >= firstDayOfLastMonth && c.AddingDate <= lastDayOfLastMonth);
                    break;

                case "Accepted":
                    course = course.Where(c => c.Status == true);
                    break;
                case "Pending":
                    course = course.Where(c => c.Status == false); ;
                    break;
                case "Paid":
                    course = course.Where(c => c.PriceStatus == false); ;
                    break;
                case "Free":
                    course = course.Where(c => c.PriceStatus == true); ;
                    break;
                case "Instructor":
                    course = course.Where(c => c.InstructorId!=null ); ;
                    break;
                case "Othertypes":
                    course = course.Where(c => c.AddedByUserId!=null); ;
                    break;
                default:
                    course = course;
                    break;
            }
            return View("CourseReports", await PaginatedListNew<Course>.CreateAsync(course.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        
        public async Task<IActionResult> FilterReviews(string filterType, int? pageNumber)
        {
            //IQueryable<Course> courses = _courseRepository.GetAll().AsQueryable();

            int pageSize = 15;
            var review = _reviewRepository.GetAllWithCourse().AsQueryable();
            switch (filterType)
            {
                case "AddedToday":
                    var today = DateTime.Today;
                    review = review.Where(c => c.RatingDate.Date == today);
                    break;
                case "LastMonth":
                    // Calculate the date range for the last month
                    today = DateTime.Today;
                    var firstDayOfLastMonth = new DateTime(today.Year, today.Month - 1, 1);
                    var lastDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
                    review = review.Where(c => c.RatingDate >= firstDayOfLastMonth && c.RatingDate <= lastDayOfLastMonth);
                    break;

                case "Accepted":
                    review = review.Where(c => c.Status == true);
                    break;
                case "Pending":
                    review = review.Where(c => c.Status == false); ;
                    break;
                default:
                    review = review;
                    break;
            }
            return View("ReviewReports", await PaginatedListNew<Review>.CreateAsync(review.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        
        public async Task<IActionResult> FilterInstructors(string filterType, int? pageNumber)
        {
            //IQueryable<Course> courses = _courseRepository.GetAll().AsQueryable();

            int pageSize = 15;
            var instructor = _instructorRepository.GetAll().AsQueryable();
            switch (filterType)
            {
                case "AddedToday":
                    var today = DateTime.Today;
                    instructor = instructor.Where(c => c.InstructorNavigation.RegisterDate == today);
                    break;
                case "LastMonth":
                    // Calculate the date range for the last month
                    today = DateTime.Today;
                    var firstDayOfLastMonth = new DateTime(today.Year, today.Month - 1, 1);
                    var lastDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
                    instructor = instructor.Where(c => c.InstructorNavigation.RegisterDate >= firstDayOfLastMonth && c.InstructorNavigation.RegisterDate <= lastDayOfLastMonth);
                    break;

                case "Male":
                    instructor = instructor.Where(c => c.InstructorNavigation.Gender == Helpers.Enums.Gender.Male);
                    break;
                case "Female":
                    instructor = instructor.Where(c => c.InstructorNavigation.Gender == Helpers.Enums.Gender.Female); ;
                    break;
                default:
                    instructor = instructor;
                    break;
            }
            return View("instructorReports", await PaginatedListNew<Instructor>.CreateAsync(instructor.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        
        public async Task<IActionResult> FilterStudents(string filterType, int? pageNumber)
        {
            //IQueryable<Course> courses = _courseRepository.GetAll().AsQueryable();

            int pageSize = 15;
            var student = _studentRepository.GetAll().AsQueryable();
            switch (filterType)
            {
                case "AddedToday":
                    var today = DateTime.Today;
                    student = student.Where(c => c.StudentNavigation.RegisterDate == today);
                    break;
                case "LastMonth":
                    // Calculate the date range for the last month
                    today = DateTime.Today;
                    var firstDayOfLastMonth = new DateTime(today.Year, today.Month - 1, 1);
                    var lastDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
                    student = student.Where(c => c.StudentNavigation.RegisterDate >= firstDayOfLastMonth && c.StudentNavigation.RegisterDate <= lastDayOfLastMonth);
                    break;

                case "Male":
                    student = student.Where(c => c.StudentNavigation.Gender == Helpers.Enums.Gender.Male);
                    break;
                case "Female":
                    student = student.Where(c => c.StudentNavigation.Gender == Helpers.Enums.Gender.Female); ;
                    break;
                default:
                    student = student;
                    break;
            }
            return View("StudentReports", await PaginatedListNew<Student>.CreateAsync(student.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
    }
}
