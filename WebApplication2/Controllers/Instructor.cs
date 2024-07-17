using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class InstructorController : Controller
    {
       private readonly LconsultDBContext _context;
       private readonly ICourseRepository _courseRepository;
       private readonly IReviewRepository _reviewRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IInstructorRepository _instructorRepository;
        private readonly UserManager<User> _userManager;
        public InstructorController(LconsultDBContext context,ICourseRepository Course, IWebHostEnvironment environment,IReviewRepository reviewRepository,IInstructorRepository instructorRepository,UserManager<User> userManager  )
        {
            _context = context;
            _courseRepository = Course;
            _environment = environment;
            _reviewRepository = reviewRepository;
            _instructorRepository = instructorRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> GetSubcategories(int categoryId)
        {
            var subcategories = await _context.SubCategories
                .Where(s => s.CategoryId == categoryId)
                .Select(s => new { s.SubId, s.SubName })
                .ToListAsync();

            return Json(subcategories);
        }

        
        //[HttpGet]
        //public async Task<JsonResult> GetProviders(string search)
        //{
        //    var providers =await _context.Courses
        //        .Where(p => p.Platform.Contains(search))  // Implement more complex search logic if necessary
        //        .Select(p => p.Platform).Distinct()
        //        .ToListAsync();

        //    return  Json(providers);
        //}
        public IActionResult test()
        {
           string provider= _context.Courses.FirstOrDefault().Platform?.ToString()??"NA";
            return Content(provider);
        }

        public IActionResult Index()
        {

            var _topCourses = _courseRepository.GetTopCourses();
            var _topReviews = _reviewRepository.GetTopReviews();

            IndexViewModel top = new IndexViewModel
            {
                Course = _topCourses,
                Review = _topReviews
            };
            return View(top);
        }
        public IActionResult MyCourses()
        {
            return View();
        }        
        public IActionResult Profile(string id)
        {
            var instructor = _instructorRepository.GetById(id);
           
            var _instructor = new InstructorDetailsViewModel
            {
                UserName=instructor.InstructorNavigation.UserName,
                Email =instructor.InstructorNavigation.Email,
                PhoneNumber = instructor.InstructorNavigation.PhoneNumber,
                FirstName=instructor.InstructorNavigation.FirstName,
                LastName =instructor.InstructorNavigation.LastName,
                Gender =instructor.InstructorNavigation.Gender,
                Profession=instructor.Profession,
                YearsExperince=instructor.YearsExperince,
                Website=instructor.Website,
                About=instructor.About,
            };
            return View(_instructor);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var instructor = _instructorRepository.GetById(id);
            if(instructor is not null)
            {
                EditInstructorViewModel _instructor = new()
                {
                     InstructorId= instructor.InstructorId,
                     UserName = instructor.InstructorNavigation.UserName,
                     Email= instructor.InstructorNavigation.Email,
                     OriginalEmail= instructor.InstructorNavigation.Email,
                     FirstName= instructor.InstructorNavigation.FirstName,
                     LastName=instructor.InstructorNavigation.LastName,
                     PhoneNumber=instructor.InstructorNavigation.PhoneNumber,
                     Gender=instructor.InstructorNavigation.Gender,
                     Profession = instructor.Profession,
                     YearsExperince=instructor.YearsExperince,
                     About = instructor.About,
                     Website = instructor.Website,


                };
                return View(_instructor);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditInstructorViewModel model)
        {
            Instructor _instructor = _instructorRepository.GetById(model.InstructorId);

            _instructor.InstructorNavigation.UserName= model.UserName;
            _instructor.InstructorNavigation.Email= model.Email;
            _instructor.InstructorNavigation.NormalizedEmail = model.Email.ToUpper();
            _instructor.InstructorNavigation.FirstName= model.FirstName;
            _instructor.InstructorNavigation.LastName= model.LastName;
            _instructor.InstructorNavigation.Gender= model.Gender;
            _instructor.InstructorNavigation.PhoneNumber= model.PhoneNumber;
            _instructor.Profession= model.Profession;
            _instructor.Website= model.Website;
            _instructor.YearsExperince= model.YearsExperince;
            _instructor.About= model.About;

            _instructorRepository.Update(_instructor);
            _instructorRepository.Save();
            TempData["Success"] = "Instructor data edited successfully!";
            return RedirectToAction("index");

           
        }

        [HttpGet]
        public IActionResult AddCourse()
        { 
            ViewBag.Categories= _context.Categories;
            ViewBag.Languages= _context.Languages;
            return View();
        }



        [HttpPost]
        public IActionResult AddCourse(AddCourseViewModel model)
        {
            string uniqeFileName = ProcessUploadFile(model);
            if (ModelState.IsValid)
            {
                Course newCourse = new()
                {
                Title = model.Title,
                CategoryId = model.CategoryId,
                AddingDate = model.AddingDate,
                AverageRating = model.AverageRating,
                LastUpdate = model.LastUpdate,
                CourseDuration = model.CourseDuration,
                CourseDescription = model.CourseDescription,
                LanguageId = model.LanguageId,
                Level = model.Level,
                PriceStatus = model.PriceStatus,
                SubcategoryId = model.SubcategoryId,
                TopicsCovered = model.TopicsCovered,
                    Link = model.Link,
                    VedioLength = model.VedioLength,
                    Picture=uniqeFileName,
                };
                _courseRepository.Add(newCourse);
            }
            return RedirectToAction("AddCourse");
        }

        private string ProcessUploadFile(AddCourseViewModel model)
        {
            string uniqeFileName = null;
            if (model.Picture is not null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images/users");
                uniqeFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string filePath = Path.Combine(uploadFolder, uniqeFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStream);
                }

            }

            return uniqeFileName;
        }
        public IActionResult InputPartial()
        {
            return PartialView("_SocialMediaAccountsPartial");
        }

            [AcceptVerbs("Get", "Post")]
    public async Task< IActionResult> VerifyEmail(string email, string originalEmail)
    {
            
            if ( email == originalEmail)
        {
            return Json(true);
        }

            //var emailExists = _instructorRepository.GetAll()
            //                    .Any(u => u.InstructorNavigation.Email == email);
            User emailExists = await _userManager.FindByEmailAsync(email);
            if (emailExists !=null)
        {
            return Json($"The email {email} is already in use.");
        }

        return Json(true);
    }
    }
}
