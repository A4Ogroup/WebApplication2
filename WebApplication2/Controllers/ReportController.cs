using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
         LconsultDBContext _context;
         IReportRepository _reportRepository;
        IReviewRepository _reviewRepository;
        private readonly   UserManager<User>  _userManager;

        public ReportController(LconsultDBContext context, IReportRepository reportRepository, IReviewRepository reviewRepository, UserManager<User> userManager)
        {
            _context = context;
            _reportRepository = reportRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult AddReport(int id) {

            var studentId = _userManager.GetUserId(User);
            var existingReport = _reportRepository.GetAll()
            .FirstOrDefault(r => r.ReviewId == id && r.StudentId == studentId);
            if (existingReport != null)
            {
                TempData["Failed"] = "You have already added a report for this review!!";

                return RedirectToAction("Index", "Student", new { id = id });
            }

            var model = new AddReportViewModel { ReviewId = id };
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddReport(AddReportViewModel report)
        {
            //var _report= _context.Reviews.FirstOrDefault(r => r.ReviewId==report.ReviewId);
            //if (_report==null)
            //{
            //    return RedirectToAction("index","home");
            //}
            if (ModelState.IsValid)
            {

                Report _report1 = new()
                {

                    Message = report.Message,
                    ReviewId = report.ReviewId,
                };
                _reportRepository.Add(_report1);
                _reportRepository.Save();
                TempData["Success"] = "Report added successfully!";
            }
            return RedirectToAction("index","student");
        }
        [HttpGet]
        public IActionResult EditReport(int id)
        {
            var report = _reportRepository.GetById(id);
            if (report != null)
            {
                EditReportViewModel model = new()
                {
                    ReportId = report.ReportId,
                    Message = report.Message,
                    ReportDate = report.ReportDate,
                    ReviewId = report.ReviewId,
                };
                return View(model);
            }

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditReport(EditReportViewModel model,int Id)
        {
            Report report = _reportRepository.GetById(model.ReportId);

            report.Message = model.Message;
            report.ReviewId = model.ReviewId;

            _reportRepository.Update(report);
            _reportRepository.Save();
            return RedirectToAction("index","student");
        }

        public IActionResult Delete(int id) 
        { 
            _reportRepository.Delete(id);
            _reportRepository.Save();
            TempData["Success"] = "Report deleted successfully!";
            return RedirectToAction("index", "admin");
        }
    }
}
