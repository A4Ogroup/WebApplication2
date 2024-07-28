using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{

    [Authorize]
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

        public async Task<IActionResult> MyCourses(int? pageNumber)
        {
            int pageSize = 15;
            var instructortId = _userManager.GetUserId(User);
         
            var mycourses = _courseRepository.GetAllWithLanguageAddedByInstructor(instructortId).AsQueryable();
            return View(await PaginatedListNew<Course>.CreateAsync(mycourses.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        //public IActionResult MyCourses()
        //{
        //    var instructortId = _userManager.GetUserId(User);
        //    var mycourses = _courseRepository.GetAllWithLanguage().Where(i => i.InstructorId == instructortId);
        //    return View(mycourses);
        //}        
        public IActionResult Profile()
        {
            var instructortId = _userManager.GetUserId(User);
            var instructor = _instructorRepository.GetById(instructortId);

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

        public async Task<IActionResult> ViewProfile(int? pageNumber, string instructorId)
        {

            var instructor = _instructorRepository.GetById(instructorId);
            int pageSize = 15;
            var ViewCourses = _courseRepository.GetAllWithLanguageAddedByInstructor(instructorId).AsQueryable();
            var PagedCourse = await PaginatedListNew<Course>.CreateAsync(ViewCourses.AsNoTracking(), pageNumber ?? 1, pageSize);


            var _instructor = new InstructorDetailsViewModel
            {
                InstructorId = instructorId,
                UserName = instructor.InstructorNavigation.UserName,
                Email = instructor.InstructorNavigation.Email,
                PhoneNumber = instructor.InstructorNavigation.PhoneNumber,
                FirstName = instructor.InstructorNavigation.FirstName,
                LastName = instructor.InstructorNavigation.LastName,
                Gender = instructor.InstructorNavigation.Gender,
                Profession = instructor.Profession,
                YearsExperince = instructor.YearsExperince,
                Website = instructor.Website,
                About = instructor.About,
                Course = PagedCourse,
           
            };
            return View(_instructor);
        }
        [HttpGet]
        public IActionResult Edit()
        {
            var instructortId = _userManager.GetUserId(User);
            var instructor = _instructorRepository.GetById(instructortId);
            if(instructor is not null)
            {
                EditInstructorViewModel _instructor = new()
                {
                    InstructorId = instructortId,
                    UserName = instructor.InstructorNavigation.UserName,
                    OriginalUserName = instructor.InstructorNavigation.UserName,
                    Email = instructor.InstructorNavigation.Email,
                    OriginalEmail = instructor.InstructorNavigation.Email,
                    FirstName = instructor.InstructorNavigation.FirstName,
                    LastName = instructor.InstructorNavigation.LastName,
                    PhoneNumber = instructor.InstructorNavigation.PhoneNumber,
                    Gender = instructor.InstructorNavigation.Gender,
                    Profession = instructor.Profession,
                    YearsExperince = instructor.YearsExperince,
                    About = instructor.About,
                    Website = instructor.Website,
                    SocialMediaAccounts = instructor.SocialMediaAccounts.Select(s => s.Account ).ToList(),

                     ExistingPhotoPath=instructor.InstructorNavigation.Picture

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
            _instructor.Website= model.Website;
            
                if (model.Picture != null)
            {

                if (model.ExistingPhotoPath != null)
                {
                    string filePath = Path.Combine(_environment.WebRootPath, "images/users", model.ExistingPhotoPath);
                    System.IO.File.Delete(filePath);
                }
                _instructor.InstructorNavigation.Picture = ProcessUploadFile(model, x => x.Picture);

            }
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
            string uniqeFileName = ProcessUploadFile(model, x => x.Picture);
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

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> VerifyUserName(string userName, string originalUserName)
        {
            // Check if the new username is the same as the original
            if (userName == originalUserName)
            {
                return Json(true);
            }

            // Check if the username already exists
            User userNameExists = await _userManager.FindByNameAsync(userName);
            if (userNameExists != null)
            {
                return Json($"The UserName {userName} is already in use.");
            }

            return Json(true);
        }


        private string ProcessUploadFile<T>(T model, Func<T, IFormFile> pictureAccessor)
        {
            string uniqeFileName = null;
            var picture = pictureAccessor(model);
            if (picture is not null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images/users");
                uniqeFileName = Guid.NewGuid().ToString() + "_" + picture.FileName;
                string filePath = Path.Combine(uploadFolder, uniqeFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    picture.CopyTo(fileStream);
                }
            }

            return uniqeFileName;
        }

    }
}
