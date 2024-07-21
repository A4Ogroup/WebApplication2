using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class StudentController : Controller
    {
        private readonly LconsultDBContext _context;
        private readonly ICourseRepository _courseRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentController(LconsultDBContext context , IReviewRepository reviewRepository,ICourseRepository courseRepository,IStudentRepository studentRepository)
        {
            _context = context;
            _reviewRepository = reviewRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }
        public IActionResult Index()
        {
            var _topCourses = _courseRepository.GetTopCourses();
            var _topReviews = _reviewRepository.GetTopReviews();
          

            IndexViewModel top = new IndexViewModel
            {
                Course = _topCourses,
                Review = _topReviews,
               
            };
            return View(top);
        }

        public IActionResult Profile(string id)
        {
            var student = _studentRepository.GetById(id);

            var _student = new StudentDetailsViewModel
            {
                UserName = student.StudentNavigation.UserName,
                Email = student.StudentNavigation.Email,
                FirstName = student.StudentNavigation.FirstName,
                LastName = student.StudentNavigation.LastName,
                Gender = student.StudentNavigation.Gender,
                
            };
            return View(_student);
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var student = _studentRepository.GetById(id);
            if (student is not null)
            {
                EditStudentViewModel _student = new()
                {
                    StudentId= student.StudentId,
                    UserName = student.StudentNavigation.UserName,
                    OriginalUserName=student.StudentNavigation.UserName,
                    Email = student.StudentNavigation.Email,
                    OriginalEmail = student.StudentNavigation.Email,
                    FirstName = student.StudentNavigation.FirstName,
                    LastName = student.StudentNavigation.LastName,
                    Gender = student.StudentNavigation.Gender,
                };
                return View(_student);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditStudentViewModel model)
        {
            Student _student = _studentRepository.GetById(model.StudentId);

            _student.StudentNavigation.UserName = model.UserName;
            _student.StudentNavigation.Email = model.Email;
            _student.StudentNavigation.NormalizedEmail = model.Email.ToUpper();
            _student.StudentNavigation.FirstName = model.FirstName;
            _student.StudentNavigation.LastName = model.LastName;
            _student.StudentNavigation.Gender = model.Gender;

            _studentRepository.Update(_student);
            _studentRepository.Save();
            TempData["Success"] = "Student data edited successfully!";
            return RedirectToAction("index");


        }
        public IActionResult Review()
        {
            return View();
        }




    }
}
