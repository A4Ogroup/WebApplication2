using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;
using WebApplication2.Helpers.Enums;

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
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddCourse(AddCourseViewModel model)
        {
            string uniqeFileName = ProcessUploadFile(model);
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
            return PartialView("_topicsInputPartialView");
        }
        

        public IActionResult Category()
        {

            return View();
        }
        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                    PriceStatus = course.PriceStatus,
                    SubcategoryId = course.SubcategoryId,
                    TopicsCovered = course.TopicsCovered,
                    Link = course.Link,
                    VedioLength = course.VedioLength,
                    // Picture = uniqeFileName,
                };
                return View(editCourseViewModel);
            }

            return View();
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
