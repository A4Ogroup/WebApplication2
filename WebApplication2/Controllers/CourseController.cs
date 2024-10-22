﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Helpers;
using WebApplication2.Helpers.Enums;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ResourceParameters;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{

    [Authorize]
    public class CourseController : Controller
    {
        // GET: CourseController
        private readonly LconsultDBContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ICourseRepository _courseRepository;
        private readonly ISearchResultService _searchResultService;
        private readonly UserManager<User> _userManager;
        const int pageSize = 6;
        private IQueryable<Course> _courses;

        public CourseController(LconsultDBContext context, IWebHostEnvironment environment, ICourseRepository Course, ISearchResultService searchResultService, UserManager<User> userManager)
        {
            _context = context;
            _courseRepository = Course;
            _searchResultService = searchResultService;
            _environment = environment;
            _userManager = userManager;
            //_courses = _courseRepository?.GetAll() as IQueryable<Course>;


        }


        //public ActionResult Index()
        //{
        //    return View();
        //}



        public async Task<IActionResult> GetSubcategories(int categoryId)
        {
            var subcategories = await _context.SubCategories
                .Where(s => s.CategoryId == categoryId)
                .Select(s => new { s.SubId, s.SubName })
                .ToListAsync();

            return Json(subcategories);
        }
        [HttpGet]
        [InstructorVerified]
        public async Task< IActionResult> AddCourse()
        {
            //ViewBag.Categories = _context.Categories;
            //ViewBag.Languages = _context.Languages;

            var category = _context.Categories;
            var language = _context.Languages;
            var user = await _userManager.GetUserAsync(User);
            if (user != null && await _userManager.IsInRoleAsync(user, "Instructor"))
            {
                var instructorName = $"{user.FirstName} {user.LastName}";
                ViewBag.InstructorName = instructorName;
            }

            var model = new AddCourseViewModel
            {
                Categories = category,
                Languages = language,
            };
            

            return View(model);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddCourse(AddCourseViewModel model)
        {
            string uniqeFileName = ProcessUploadFile(model, x => x.Picture);

            //string uniqeFileName = ProcessUploadFile(model);
            //ViewBag.Categories = _context.Categories;
            //ViewBag.Languages = _context.Languages;

            string idToSave = null;
            bool _status = true;
            bool _claim =true;

            if (User.IsInRole("Student")|| User.IsInRole("Admin"))
            {
                idToSave = model.AddedByUserId;

            }
            else if (User.IsInRole("Instructor"))
            {
                idToSave = model.InstructorId;
                model.Status = _status;
                model.Claimed = _claim;
                
            }
            if (ModelState.IsValid && idToSave!=null)
            {
                Course newCourse = new()
                {
                    Title = model.Title,
                    AddedByUserId =model.AddedByUserId,
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
                    Status = model.Status,
                    Platform = model.Platform,
                    InstructorId=model.InstructorId,
                    Claimed= model.Claimed,
                    
                };
                _courseRepository.Add(newCourse);
                _courseRepository.Save();
                
                TempData["Success"] = "Course added successfully!";
                return RedirectToAction("index", "instructor");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return RedirectToAction("addcourse", "instructor");
        }
        


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
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, int? pageNumber, double[] ratings = null, string[] sortDates = null)
        {
            var course = _courseRepository.GetAllWithLanguage()
                .FirstOrDefault(c => c.CourseId == id);

            int pageSize = 6;
            var reviews = _context.Reviews.Include(r => r.Student).ThenInclude(s => s.StudentNavigation)
                .Where(r => r.CourseId == id && r.Status == true);

            // Filter by minimum rating
            if (ratings != null && ratings.Length > 0)
            {
                var minRating = ratings.Min(); // Get the minimum rating from selected filters
                reviews = reviews.Where(r => r.Rate >= minRating);
            }

            // Sort by date
            if (sortDates != null && sortDates.Length > 0)
            {
                if (sortDates.Contains("newest"))
                {
                    reviews = reviews.OrderByDescending(r => r.RatingDate);
                }
                else if (sortDates.Contains("oldest"))
                {
                    reviews = reviews.OrderBy(r => r.RatingDate);
                }
            }

            var CourseReview = new CourseDetailsViewModel
            {
                Course = course,
                PaginatedReviews = await PaginatedListNew<Review>.CreateAsync(reviews.AsNoTracking(), pageNumber ?? 1, pageSize)
            };

            return View(CourseReview);
        }


        // GET: CourseController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: CourseController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: CourseController/Edit/5
        public IActionResult EditCourse(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course is not null)
            {
                EditCourseViewModel editCourseViewModel = new()
                {
                    CourseId = course.CourseId,
                    Title = course.Title,
                    CategoryId = course.CategoryId.Value,
                    AddingDate = course.AddingDate,
                    AverageRating = course.AverageRating,
                    LastUpdate = course.LastUpdate.Value,
                    InstructorFullName = course.InstructorFullName,
                    CourseDuration = course.CourseDuration.Value,
                    CourseDescription = course.CourseDescription,
                    LanguageId = course.LanguageId.Value,
                    Level = course.Level.Value,
                    Platform = course.Platform,
                    PriceStatus = course.PriceStatus.Value,
                    SubcategoryId = course.SubcategoryId,
                    TopicsCovered = course.TopicsCovered,
                    Link = course.Link,
                    VedioLength = course.VedioLength.Value,
                    ExistingPhotoPath = course.Picture,
                };
                return View(editCourseViewModel);
            }

            return View();
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(EditCourseViewModel model)
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
                course.Picture = ProcessUploadFile(model, x => x.Picture);

            }
            _courseRepository.Update(course);
            _courseRepository.Save();
            TempData["Success"] = "Course edited successfully!";
            return RedirectToAction("index", "instructor");


        }

        [AllowAnonymous]
        public IActionResult Index1(CourseResourceParameters parameters, int pg = 1)
        {
            ViewBag.languages = _context.Languages.ToList();
            //////paging
            _courses = _courseRepository.GetCourses(parameters).AsQueryable();

            if (pg < 1)
                pg = 1;
            int recordCount = _courses?.Count() ?? 0;
            
            var pager = new Pager(recordCount, pg, pageSize);
            // int recordSkip = (pg - 1) * pageSize;
            //var resultedCourses = _courses.Select(course=> new CourseSearchResultViewModel{
            //CourseId = course.CourseId,
            //    Title = course.Title,
            //    CourseDescription = course.CourseDescription,
            //Claimed= course.Claimed,
            //InstructorFullName = course.InstructorFullName,
            //InstructorId = course.InstructorId,
            //Picture = course.Picture,
            //TopicsCovered= course.TopicsCovered,
            //AverageRating= course.AverageRating,
            //CourseDuration= course.CourseDuration,
            //LanguageId= course.LanguageId,
            //LastUpdate= course.LastUpdate,
            //Level = course.Level,
            //Link = course.Link,
            //PriceStatus= course.PriceStatus,
            //Status= course.Status,
            //VedioLength = course.VedioLength
            //});

            ViewBag.courses = _courses.Skip((pg - 1) * pageSize).Take(pager.PageSize).ToList();
            ViewBag.Pager = pager;
            ViewBag.recordCount = recordCount;
            var model = new CourseFilterViewModel
            {

                //searchParameters = parameters, 
                //Courses = _courses,
                Ratings = new List<double>(),
                CategoryIds = new List<byte>(),
                LanguageIds = new List<int>(),
                Levels = new List<Level>(),
                VideoLengths = new List<VideoLengthCategory>(),
                IsFree = new List<bool>()
            };
            TempData["searchParam"] = parameters.SearchQuery;
            TempData["categoryId"] = parameters.CategoryId;
            //var options = new JsonSerializerOptions
            //{
            //    MaxDepth = 64, // Default, adjust if necessary after testing
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            //    WriteIndented = true // Set to false in production for better performance
            //};
            //var serializedCourses = JsonSerializer.Serialize(resultedCourses, options);
            //HttpContext.Session.SetString("Courses", serializedCourses);
            // TempData["searchParameters"] = parameters;
            return View("filter2", model);
        }

        [AllowAnonymous]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyFilters([FromBody] FilterRequest filterRequest)
        {
            try
            {
                var filters = filterRequest.Filters;
                var pageNumber = filterRequest.PageNumber;
                if (pageNumber < 1)
                    pageNumber = 1;
                //var serializedCourses = HttpContext.Session.GetString("Courses");
                //if (string.IsNullOrEmpty(serializedCourses))
                //{
                //    return BadRequest("Session expired or data not found."); 
                //}
                //var courses = JsonSerializer.Deserialize<List<CourseSearchResultViewModel>>(serializedCourses).AsQueryable();
                TempData.Keep("searchParam");
                TempData.Keep("categoryId");
                var courses = _courseRepository.GetCourses(TempData["searchParam"].ToString(), (int)TempData["categoryId"]);
                
                TempData.Keep("searchParam");
                TempData.Keep("categoryId");
                var filteredCourses = _courseRepository.FilterCourses(filters, courses).ToList();
                if (filters.Ratings != null && filters.Ratings.Any())
                    filteredCourses = filteredCourses.AsEnumerable().Where(c => filters.Ratings.Any(r => r <= c.AverageRating)).ToList();
               var recordCount = filteredCourses?.Count() ?? 0;
                ViewBag.recordeCount =recordCount;
                var pager = new Pager(filteredCourses.Count(), pageNumber, pageSize);
                var paginatedCourses = filteredCourses.Skip((pageNumber - 1) * pager.PageSize).Take(pager.PageSize).ToList();
                Console.WriteLine(paginatedCourses.Count());
                TempData.Keep("searchParam");
                TempData.Keep("categoryId");
                ViewBag.Pager = pager;
                return PartialView("_CourseListPartial", paginatedCourses);
            }
            catch (Exception ex)
            {
                // Log the error (you could use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error applying filters: {ex.Message}");
                Console.WriteLine(ex.StackTrace);  // Log the stack trace
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }



        // GET: CourseController/Delete/5
        //[HttpGet]
        public IActionResult EditableDetails(int id)
        {
            var _course = _courseRepository.GetAllWithLanguage().FirstOrDefault(c => c.CourseId == id);
            var course = new CourseDetailsViewModel
            {
                Course = _course
            };
            return View(course);
        }

        // POST: CourseController/Delete/5
       //[HttpPost]
       // [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            _courseRepository.Delete(id);
            _courseRepository.Save();
            TempData["Success"] = "Course deleted successfully!";

            if(User.IsInRole("Admin"))
            return RedirectToAction("index", "admin");


            return RedirectToAction("index","Instructor");
            //try
            //{

            //}
            //catch
            //{
            //    return View();
            //}
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> SubmitClaim(CourseClaimReportViewModel model)
        {
            var course = _courseRepository.GetById(model.CourseId);
            if (course == null || course.Claimed)
            {
                return NotFound();
            }

            var instructor = await _userManager.GetUserAsync(User);
            if (instructor == null)
            {
                return Unauthorized();
            }

            var claimReport = new CourseClaimReport
            {
                CourseId = model.CourseId,
                InstructorId = instructor.Id,
                Reason = model.Reason,
                SubmittedDate = DateTime.UtcNow
            };

            _context.CourseClaimReports.Add(claimReport);
            _context.SaveChanges();
            TempData["Success"] = "Course claim sent successfully!";
            return RedirectToAction("Details", new { id = model.CourseId });
        }

    }
}
