using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class Instructor : Controller
    {
       private readonly LconsultDBContext _context;
       private readonly ICourseRepository _courseRepository;
        private readonly IWebHostEnvironment _environment;
        public Instructor(LconsultDBContext context,ICourseRepository Course, IWebHostEnvironment environment  )
        {
            _context = context;
            _courseRepository = Course;
            _environment = environment;
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
            var courses = _context.Courses.OrderByDescending(c => c.AverageRating)
            .Take(10).ToList();

            var reviews = _context.Reviews.OrderByDescending(c => c.Rate)
                    .Take(10).ToList();
            return View();
        }
        public IActionResult MyCourses()
        {
            return View();
        }        
        public IActionResult Profile()
        {
            return View();
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
    }
}
