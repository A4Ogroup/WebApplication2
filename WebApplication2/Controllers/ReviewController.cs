using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
namespace WebApplication2.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        public IReviewRepository _reviewRepository;
        public ICourseRepository _courseRepository;
        public LconsultDBContext _context;
        private readonly UserManager<User> _userManager;

        public ReviewController(IReviewRepository reviewRepository, LconsultDBContext context, ICourseRepository courseRepository, UserManager<User> userManager)
        {
            _reviewRepository = reviewRepository;
            _context = context;
            _courseRepository = courseRepository;
            _userManager = userManager;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult AddReview(int Id)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    TempData["Success"] = "You shuold have an account to give your opinion";
            //    return RedirectToAction("Login", "Account");
            //}
            //var studentId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = _userManager.GetUserId(User);
            var existingReview =  _reviewRepository.GetAll()
            .FirstOrDefault(r => r.CourseId == Id && r.StudentId == studentId);
            if(existingReview != null )
            {
                TempData["Failed"] = "You have already added a review for this course!!";

               return RedirectToAction("Details", "Course", new {id=Id});
            }
            var model = new AddReviewViewModel
            {
                CourseId = Id,
                StudentId=studentId
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
                    StudentId = model.StudentId,
                    Status = model.Status,

                };
              
                 _reviewRepository.Add(_review);
                _reviewRepository.Save();
                TempData["Success"] = "Review added successfully!";
                return RedirectToAction("dtails", "course", new {id = model.CourseId});
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
            var studentId = _userManager.GetUserId(User);
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
                    StudentId = studentId
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