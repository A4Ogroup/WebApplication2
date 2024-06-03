using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public TestController( LconsultDBContext context)
        {
            _context = context;
        }
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult New(CourseFilterViewModel filter )
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
            v

            if (courses == null)
            {
                return NotFound();
            }

            var searchViewModel = new SearchViewModel()
            {
                
                courses = courses ,
                languages= languages,
                


            };
            //var courses = _context.Courses.ToList();

            return View(/*courses*/);
        }
    }
}
