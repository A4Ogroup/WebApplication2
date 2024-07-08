using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class Student : Controller
    {
        private readonly LconsultDBContext _context;
        private readonly ICourseRepository _courseRepository;
        private readonly IReviewRepository _reviewRepository;

        public Student (LconsultDBContext context , IReviewRepository reviewRepository,ICourseRepository courseRepository)
        {
            _context = context;
            _reviewRepository = reviewRepository;
            _courseRepository = courseRepository;
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
