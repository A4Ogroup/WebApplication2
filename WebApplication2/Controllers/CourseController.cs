using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;
using WebApplication2.Helpers.Enums;
using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebApplication2.Controllers
{
    public class CourseController : Controller
    {
        // GET: CourseController
        private readonly LconsultDBContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ICourseRepository _courseRepository;
        public CourseController( LconsultDBContext context, IWebHostEnvironment environment,ICourseRepository Course)
        {
            _context = context;
            _courseRepository = Course;
            _environment = environment;

        }
        public ActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> GetSubcategories(int categoryId)
        {
            var subcategories = await _context.SubCategories
                .Where(s => s.CategoryId == categoryId)
                .Select(s => new { s.SubId, s.SubName })
                .ToListAsync();

            return Json(subcategories);
        }
        [HttpGet]
        public IActionResult AddCourse()
        {
            ViewBag.Categories = _context.Categories;
            ViewBag.Languages = _context.Languages;
            return   View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddCourse(AddCourseViewModel model)
        {
            string uniqeFileName = ProcessUploadFile(model, x => x.Picture);

            //string uniqeFileName = ProcessUploadFile(model);
            ViewBag.Categories = _context.Categories;
            ViewBag.Languages = _context.Languages;
            if (ModelState.IsValid)
            {
                Course newCourse = new()
                {
                    Title = model.Title,
                    CategoryId = model.CategoryId,
                    AddingDate = model.AddingDate,
                    AverageRating = model.AverageRating,
                    LastUpdate = model.LastUpdate,
                    InstructorFullName = model.InstructorFullName,
                    CourseDuration = model.CourseDuration,
                    CourseDescription = model.CourseDescription,
                    LanguageId = model.LanguageId,
                    Level = model.Level,
                    PriceStatus = model.PriceStatus,
                    SubcategoryId = model.SubcategoryId,
                    TopicsCovered = model.TopicsCovered,
                    Link = model.Link,
                    VedioLength = model.VedioLength,
                    Picture = uniqeFileName,
                };
                _courseRepository.Add(newCourse);
                _courseRepository.Save();
            }
            return RedirectToAction("AddCourse");
        }

        //private string ProcessUploadFile(AddCourseViewModel model)
        //{
        //    string uniqeFileName = null;
        //    if (model.Picture is not null)
        //    {
        //        string uploadFolder = Path.Combine(_environment.WebRootPath, "images/users");
        //        uniqeFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
        //        string filePath = Path.Combine(uploadFolder, uniqeFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.Picture.CopyTo(fileStream);
        //        }

        //    }

        //    return uniqeFileName;
        //}

        private string ProcessUploadFile<T>(T model, Func<T, IFormFile> pictureAccessor)
        {
            string uniqeFileName = null;
            var picture = pictureAccessor(model);
            if (picture is not null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images/courses");
                uniqeFileName = Guid.NewGuid().ToString() + "_" + picture.FileName;
                string filePath = Path.Combine(uploadFolder, uniqeFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    picture.CopyTo(fileStream);
                }
            }

            return uniqeFileName;
        }
        public IActionResult InputPartial()
        {
            return PartialView("_topicsInputPartialView");
        }
        

        public IActionResult Category()
        {

            return View();
        }
        // GET: CourseController/Details/5
        public  IActionResult Details(int id)
        {
            var course =  _context.Courses.FirstOrDefault(c => c.CourseId == id);
       

            var review = _context.Reviews.Select(r => new
            {
                r.ReviewId,
                r.RatingDate,
                r.Student,
                r.Rate,
                r.Descritipn,
            }).ToList();

;

            return View(course);
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Edit/5
        public IActionResult EditCourse(int id)
        {
            var course = _courseRepository.GetById(id);
            if(course is not null)
            {
                EditCourseViewModel editCourseViewModel = new ()
                {
                    CourseId = course.CourseId,
                    Title = course.Title,
                    CategoryId = course.CategoryId,
                    AddingDate = course.AddingDate,
                    AverageRating = course.AverageRating,
                    LastUpdate = course.LastUpdate,
                    InstructorFullName = course.InstructorFullName,
                    CourseDuration = course.CourseDuration,
                    CourseDescription = course.CourseDescription,
                    LanguageId = course.LanguageId,
                    Level = course.Level,
                    Platform=course.Platform,
                    PriceStatus = course.PriceStatus,
                    SubcategoryId = course.SubcategoryId,
                    TopicsCovered = course.TopicsCovered,
                    Link = course.Link,
                    VedioLength = course.VedioLength,
                    ExistingPhotoPath = course.Picture,
                };
                return View(editCourseViewModel);
            }

            return View();
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse( EditCourseViewModel model)
        {
            //if(modelstate.isvalid)
            

                Course course = _courseRepository.GetById(model.CourseId);


            course.CourseId = model.CourseId;
            course.Title = model.Title;
            course.CategoryId = model.CategoryId;
            course.AddingDate = model.AddingDate;
            course.AverageRating = model.AverageRating;
            course.LastUpdate = model.LastUpdate;
            course.InstructorFullName = model.InstructorFullName;
            course.CourseDuration = model.CourseDuration;
            course.CourseDescription = model.CourseDescription;
            course.LanguageId = model.LanguageId;
            course.Level = model.Level;
            course.Platform = model.Platform;
            course.PriceStatus = model.PriceStatus;
            course.SubcategoryId = model.SubcategoryId;
            course.TopicsCovered = model.TopicsCovered;
            course.Link = model.Link;
            course.VedioLength = model.VedioLength;
            if (model.Picture != null)
            {

                if (model.ExistingPhotoPath != null)
                {
                    string filePath = Path.Combine(_environment.WebRootPath, "images/courses", model.ExistingPhotoPath);
                    System.IO.File.Delete(filePath);
                }
                course.Picture = ProcessUploadFile(model,x=>x.Picture);

            }
            _courseRepository.Update(course);
            _courseRepository.Save();
            return RedirectToAction("index","Home");
            
                
             }
        

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
