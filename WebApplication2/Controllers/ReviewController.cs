using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class ReviewController : Controller
    {
        public IReviewRepository _reviewRepository;
        public ICourseRepository _courseRepository;
        public LconsultDBContext _context;

        public ReviewController(IReviewRepository reviewRepository, LconsultDBContext context, ICourseRepository courseRepository)
        {
            _reviewRepository = reviewRepository;
            _context = context;
            _courseRepository = courseRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddReview(int Id)
        {
            var model = new AddReviewViewModel
            {
                CourseId = Id
            };
            return View(model);


            //return View("addreview", new {Id});
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddReview(AddReviewViewModel model)
        {
            //var course = _courseRepository.GetById(model.CourseId);
            //if(course  == null){
              
            //    return View("index");
            //}

            if (ModelState.IsValid)
            {
                Review _review = new()
                {
                    Descritipn = model.Descritipn,
                    Rate = model.Rate,
                    MaterialQuality = model.MaterialQuality,
                    SupportQuality = model.SupportQuality,
                    TechnicalQuality = model.TechnicalQuality,
                    ContentQuality = model.ContentQuality,
                    EngagementLevel = model.EngagementLevel,
                    OverAllSatisfication = model.OverAllSatisfication,
                    CourseId = model.CourseId,

                };
              
                 _reviewRepository.Add(_review);
                _reviewRepository.Save();
                TempData["Success"] = "Review added successfully!";

            }
            // return RedirectToAction("Details", new { id = review.ReviewId });
            return RedirectToAction("Index","student");

        }

        public IActionResult Details(int id)
        {
            var reviews = _reviewRepository.GetById(id);

            //ViewBag.SuccessMessage = TempData["SuccessMessage"];

            var review = new ReviewDetailsViewModel
            {
                ReviewId = reviews.ReviewId,
                Descritipn= reviews.Descritipn,
                Rate = reviews.Rate,
                MaterialQuality = reviews.MaterialQuality,  
                SupportQuality = reviews.SupportQuality,
                ContentQuality= reviews.ContentQuality,
                TechnicalQuality= reviews.TechnicalQuality,
                EngagementLevel = reviews.EngagementLevel,
                OverAllSatisfication= reviews.OverAllSatisfication,
                CourseId = reviews.CourseId,
            };
            return View(review);
        }

        [HttpGet]
        public IActionResult EditReview( int id)
        {
            var _review=_reviewRepository.GetById(id);

            if(_review != null)
            {
                EditReviewViewModel model = new()
                {
                    ReviewId = _review.ReviewId,
                    Descritipn = _review.Descritipn,
                    Rate = _review.Rate,
                    MaterialQuality = _review.MaterialQuality,
                    SupportQuality = _review.SupportQuality,
                    TechnicalQuality  = _review.TechnicalQuality,
                    EngagementLevel = _review.EngagementLevel,
                    ContentQuality = _review.ContentQuality,
                    OverAllSatisfication = _review.OverAllSatisfication,
                    CourseId = _review.CourseId,
                };
                
                return View(model);
            }
            return View();


        }

        [HttpPost]

        public IActionResult EditReview(EditReviewViewModel model)
        {

            Review review=_reviewRepository.GetById(model.ReviewId);

            review.ReviewId = model.ReviewId;
            review.Descritipn= model.Descritipn;
            review.Rate = model.Rate;
            review.SupportQuality = model.SupportQuality;
            review.MaterialQuality = model.MaterialQuality;
            review.ContentQuality = model.ContentQuality;
            review.EngagementLevel= model.EngagementLevel;
            review.TechnicalQuality = model.TechnicalQuality;
            review.OverAllSatisfication= model.OverAllSatisfication;
            //review.CourseId = model.CourseId;

            _reviewRepository.Update(review);
            _reviewRepository.Save();
            TempData["Success"] = "Review edited successfully!";

            return RedirectToAction("index", "student");
        }
        public IActionResult EditableDetails(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.ReviewId == id);

            return View(review);
        }
        public IActionResult Delete(int id) { 
        
            _reviewRepository.Delete(id);
            _reviewRepository.Save();
            TempData["Success"] = "Review deleted successfully!";
            return RedirectToAction("index","admin");
        }
    }
}