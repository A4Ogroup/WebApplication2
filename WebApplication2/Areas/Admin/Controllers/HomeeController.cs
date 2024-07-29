using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Repository;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using WebApplication2.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeeController : Controller
    {
        LconsultDBContext _context;
        ICourseRepository _courseRepository;
        IReviewRepository _reviewRepository;
        IReportRepository _reportRepository;
        IInstructorRepository _instructorRepository;
        IStudentRepository _studentRepository;
        IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public HomeeController(LconsultDBContext context, ICourseRepository courseRepository, IReviewRepository reviewRepository, IReportRepository reportRepository, IInstructorRepository instructorRepository, IStudentRepository studentRepository, IUserRepository userRepository,UserManager<User> userManager,SignInManager<User> signInManager)
        {

            _context = context;
            _courseRepository = courseRepository;
            _reviewRepository = reviewRepository;
            _reportRepository = reportRepository;
            _instructorRepository = instructorRepository;
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }
public async Task<string> AdminInfo()
{
    var user = await _userManager.GetUserAsync(User);
    if (user != null)
    {
        ViewBag.UserName = user.UserName;
        ViewBag.Email = user.Email;
    }
    return $"{ViewBag.UserName} - {ViewBag.Email}";
}

public async Task<ActionResult> Index()
{
    var course = await _courseRepository.GetAllAsync();
    var review = await _reviewRepository.GetAllAsync();
    var report = await _reportRepository.GetAllAsync();
    var student = await _studentRepository.GetAllAsync();
    var instructor = await _instructorRepository.GetAllAsync();
    var courseClaims =  _context.CourseClaimReports.ToList().Where(cr => cr.Status == "Pending");

    // Get admin info
    await AdminInfo();

    var model = new IndexViewModel
    {
        Course = course,
        Review = review,
        reports = report,
        students = student,
        instructors = instructor,
        CourseClaimReports= courseClaims,
       
    };

    return View(model);
}

        //public IActionResult AdminDetail(string id)
        //{
        //    var admin= _userRepository.GetAll().Where(i => i.Id==id);

        //    var _admin = new AdminDetailsViewModel();
        //    return PartialView("_AdminDetailsPartial", _admin);
        //}
        //[HttpGet]
        //public IActionResult AdminRegister()
        //{

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AdminRegister(AdminRegisterViewModel _adminModel)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        User userModel = new User();
        //        userModel.UserName = _adminModel.UserName;
        //        userModel.FirstName = _adminModel.FirstName;
        //        userModel.LastName = _adminModel.LastName;
        //        userModel.Email = _adminModel.Email;
        //        userModel.PasswordHash = _adminModel.Password;

        //        IdentityResult result = await _userManager.CreateAsync(userModel, _adminModel.Password);

        //        if (result.Succeeded == true)
        //        {
        //            await _userManager.AddToRoleAsync(userModel, "Admin");
        //            await _signInManager.SignInAsync(userModel, isPersistent: false);
        //            TempData["Success"] = "Account created successfully!";
        //            return RedirectToAction("login", "account");
        //        }
        //        else
        //        {
        //            foreach (var item in result.Errors)
        //            {
        //                ModelState.AddModelError("", item.Description);
        //            }
        //        }

        //    }
        //    return View("AdminRegister");
        //}
        public async Task<IActionResult> Course(int? pageNumber)
        {
           await AdminInfo();

            int pageSize = 15;
            var course = _courseRepository.GetAllWithLanguage().AsQueryable();
            return View(await PaginatedListNew<Course>.CreateAsync(course.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        


        public async Task<IActionResult> Review(int? pageNumber)
        {
           await AdminInfo();

            int pageSize = 15;
            var reviews = _reviewRepository.GetAllWithCourse().AsQueryable();
            return View(await PaginatedListNew<Review>.CreateAsync(reviews.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Report(int? pageNumber)
        {
           await AdminInfo();

            int pageSize = 12;
            var report = _reportRepository.GetAllWithReview().AsQueryable();
            return View(await PaginatedListNew<Report>.CreateAsync(report.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        public async Task<IActionResult> Instructor(int? pageNumber)
        {
           await AdminInfo();

            int pageSize = 15;
            var instructors = _instructorRepository.GetAll().AsQueryable();
            return View(await PaginatedListNew<Instructor>.CreateAsync(instructors.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        public async Task<IActionResult> Student(int? pageNumber)
        {
           await AdminInfo();

            int pageSize = 12;
            var student = _studentRepository.GetAll().AsQueryable();
            return View(await PaginatedListNew<Student>.CreateAsync(student.AsNoTracking(), pageNumber ?? 1, pageSize));
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

       public async Task<IActionResult> PendingClaims()
       {
            await AdminInfo();
            var pendingClaims = await _context.CourseClaimReports               
               .Include(cr => cr.Course)
               .Include(cr => cr.Instructor)
               .ThenInclude(i=>i.InstructorNavigation)
               .ToListAsync();

           return View(pendingClaims);
       }

       [HttpPost]
       [ValidateAntiForgeryToken]
       public  IActionResult ApproveClaim(int id)
       {
           var claim = _context.CourseClaimReports.Include(c=>c.Instructor).ThenInclude(s=>s.InstructorNavigation).FirstOrDefault(c=>c.Id==id);
            var course =  _courseRepository.GetById(claim.CourseId);
            if (claim != null && claim.Status == "Pending")
           {

                course.Claimed = true;
                course.InstructorId = claim.InstructorId;
                course.InstructorFullName= $"{claim.Instructor.InstructorNavigation.FirstName} {claim.Instructor.InstructorNavigation.LastName}";
                claim.Status = "Approved";

                _context.SaveChanges();
                TempData["success"] = "Course claim changed successfully";
                return RedirectToAction("PendingClaims");
            }

            return NotFound();

            
       }

       //[HttpPost]
       //[ValidateAntiForgeryToken]
       //public async Task<IActionResult> RejectClaim(int id)
       //{
       //    var claim = await _context.CourseClaimReports.FindAsync(id);
       //    if (claim == null || claim.Status != "Pending")
       //    {
       //        return NotFound();
       //    }

       //    claim.Status = "Pending";
       //    await _context.SaveChangesAsync();

       //    return RedirectToAction("PendingClaims");
       //}
    }
}
