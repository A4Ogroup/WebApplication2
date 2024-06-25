using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Repository;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {

        LconsultDBContext _context;
        ICourseRepository _courseRepository;
        IReviewRepository _reviewRepository;
        public AdminController(LconsultDBContext context,ICourseRepository courseRepository,IReviewRepository reviewRepository) {
            
            _context = context;
            _courseRepository = courseRepository; 
            _reviewRepository = reviewRepository;
        
        }
        public IActionResult Index()
        {
            var courses = _courseRepository.GetAll().ToList();
            return View(courses);
        }
        public IActionResult Review()
        {
            var review = _reviewRepository.GetAllWithCourse().ToList();
            return View(review);
        }
    }
}
