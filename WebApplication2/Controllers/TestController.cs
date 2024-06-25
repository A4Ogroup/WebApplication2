using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication2.Helpers;
using WebApplication2.Helpers.Enums;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class TestController : Controller
    {
        LconsultDBContext _context;
        ICourseRepository _courseRepository;
        private List<Course> _courses; // Replace with your data source
        const int pageSize = 6;

        public TestController(LconsultDBContext context, ICourseRepository courseRepository)
        {

            _context = context;
            _courseRepository = courseRepository;
            _courses = _courseRepository.GetAll().ToList();
        }
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult New(CourseFilterViewModel filter)
        {

            var courses = _context.Courses.AsQueryable();



            if (filter.CategoryIds != null && filter.CategoryIds.Any())
            {
                courses = courses.Where(c => filter.CategoryIds.Contains(c.CategoryId ?? 0));
            }

            if (filter.LanguageIds != null && filter.LanguageIds.Any())
            {
                courses = courses.Where(c => filter.LanguageIds.Contains(c.LanguageId ?? 0));
            }

            if (filter.Levels != null && filter.Levels.Any())
            {
                courses = courses.Where(c => filter.Levels.Contains(c.Level ?? Level.Begginer));
            }

            if (filter.VideoLengths != null && filter.VideoLengths.Any())
            {
                courses = courses.Where(c => filter.VideoLengths.Contains(c.VedioLength ?? VideoLengthCategory.Short));
            }

            filter.Courses = courses.ToList();

            // Load data for dropdowns and checkboxes
            ViewBag.Categories = _context.Courses.Select(c => c.CategoryId).Distinct().ToList();
            ViewBag.Languages = _context.Courses.Select(c => c.LanguageId).Distinct().ToList();

            return View(filter);
        }

        public IActionResult Search()
        {
            var courses = _context.Courses.ToList();
            var languages = _context.Languages.ToList();


            if (courses == null)
            {
                return NotFound();
            }

            var searchViewModel = new SearchViewModel()
            {

                courses = courses,
                languages = languages,



            };
            //var courses = _context.Courses.ToList();

            return View(searchViewModel);
        }




        public IActionResult Index(int pg=1)
        {
            
            ViewBag.languages = _context.Languages.ToList();
            //////paging
            ///
            if (pg < 1)
                pg = 1;
            int recordCount = _courses.Count();
            var pager = new Pager(recordCount, pg, pageSize);
           // int recordSkip = (pg - 1) * pageSize;
            ViewBag.courses = _courses.Skip((pg - 1) * pageSize).Take(pager.PageSize).ToList();
            ViewBag.Pager = pager;
            var model = new CourseFilterViewModel
            {
                //Courses = _courseRepository.GetAll().ToList(),
                Ratings = new List<double>(),
                CategoryIds = new List<byte>(),
                LanguageIds = new List<int>(),
                Levels = new List<Level>(),
                VideoLengths = new List<VideoLengthCategory>(),
                IsFree = new List<bool>()
            };

            return View("filter2", model);
        }

        [HttpPost]
        public IActionResult ApplyFilters([FromBody] FilterRequest filterRequest)
        {
            var filters=filterRequest.Filters;
            var pageNumber = filterRequest.PageNumber;
            var filteredCourses = FilterCourses(filters).ToList();
            var pager = new Pager(filteredCourses.Count(), pageNumber, pageSize);
            var paginatedCourses = filteredCourses.Skip((pageNumber - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            ViewBag.Pager=pager;
            return PartialView("_CourseListPartial", paginatedCourses);
        }

        private IQueryable<Course> FilterCourses(CourseFilterViewModel filters)
        {
            var filteredCourses = _courses.AsQueryable();


            if (filters.IsFree != null)
            {
                if(filters?.IsFree?.Count()==1)
                    filteredCourses = filteredCourses.Where(c => c.PriceStatus == filters.IsFree[0]);

               
            }

            //if (filters.IsFree != null && filters.IsFree.Contains(false))
            //{
            //    filteredCourses = filteredCourses.Where(c => c.PriceStatus == false);
            //}
            if (filters.Ratings != null && filters.Ratings.Any())
            {
                filteredCourses = filteredCourses.Where(c => filters.Ratings.Any(r => r <= c.AverageRating));
            }

            if (filters.CategoryIds != null && filters.CategoryIds.Any())
            {
                filteredCourses = filteredCourses.Where(c => filters.CategoryIds.Contains(c.CategoryId ?? 0));
            }

            if (filters.LanguageIds != null && filters.LanguageIds.Any())
            {
                filteredCourses = filteredCourses.Where(c => filters.LanguageIds.Contains(c.LanguageId ?? 0));
            }

            if (filters.Levels != null && filters.Levels.Any())
            {
                filteredCourses = filteredCourses.Where(c => filters.Levels.Contains(c.Level ?? 0));
            }

            if (filters.VideoLengths != null && filters.VideoLengths.Any())
            {
                filteredCourses = filteredCourses.Where(c => filters.VideoLengths.Contains(c.VedioLength ?? 0));
            }

            return filteredCourses;
        }




    }
}
