using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class ReviewController : Controller
    {
        public IReviewRepository _reviewRepository;
        public LconsultDBContext _context;

        public ReviewController(IReviewRepository reviewRepository, LconsultDBContext context)
        {
            _reviewRepository = reviewRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddReview()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddReview(AddReviewViewModel model)
        {

            Review _review = new()
            {
                Descritipn = model.Descritipn,
                Rate = model.Rate,

            };

            _reviewRepository.Add(_review);

            return RedirectToAction("AddReview");
        }

    }
}
