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
        public AdminController(LconsultDBContext context,ICourseRepository courseRepository,IReviewRepository reviewRepository) {
            
            _context = context;
            _courseRepository = courseRepository; 
            _reviewRepository = reviewRepository;
        
        }
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 12;
            var reviews = _context.Reviews.Include(r => r.Course);
            return View(await PaginatedList<Review>.CreateAsync(reviews.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        public IActionResult Course()
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
