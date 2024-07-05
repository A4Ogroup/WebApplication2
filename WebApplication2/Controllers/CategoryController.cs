using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;
using static WebApplication2.ViewModels.PagenationViewModel;

namespace WebApplication2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly LconsultDBContext _context;
        public CategoryController(ILogger<CategoryController> logger, ICourseRepository courseRepository, ICategoryRepository categoryRepository, LconsultDBContext consultDBContext)
        {
            _logger = logger;
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _context = consultDBContext;
        }



        public IActionResult Index()
        {
            return View("Category");
        }


        public async Task<IActionResult> Details(int categoryId, int? pageNumber) 
        {
            int pageSize = 6;
            if (!_categoryRepository.CategoryExists(categoryId)) 
            {

             return NotFound();
               
            }
            //if (!_categoryRepository.SubCategoryExists(subCategoryId))
            //{

            //    return NotFound();

            //}


            var courses = _courseRepository.GetCoursesByCategory(categoryId).AsQueryable();

            //var courses = _context.Courses.Where(c=>c.CategoryId==categoryId);
            if (courses == null)
            {
                return NotFound();
            }

            ViewBag.categoryName = _categoryRepository.GetById(categoryId).CategoryName;
            ViewBag.subCategories = _categoryRepository.GetSubCategories(categoryId);
            //var categoryViewModel = new CategoryViewModel()
            //{
            //    categoryName = _categoryRepository.GetById(categoryId).CategoryName,
            //    courses = courses,
            //    subCategories = _categoryRepository.GetSubCategories(categoryId)


            //};



           return View(await PaginatedList<Course>.CreateAsync(courses.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(categoryViewModel);
        }
        public async Task<IActionResult> FilterBySubCategory(int categoryId, int? pageNumber, int subCategoryId)
        {
            int pageSize = 6;
            if (!_categoryRepository.CategoryExists(categoryId))
            {

                return NotFound();

            }
            if (!_categoryRepository.SubCategoryExists(subCategoryId))
            {

                return NotFound();

            }


            var courses = _courseRepository.GetCoursesByCategory(categoryId).AsQueryable();
            courses = courses.Where(c => c.SubcategoryId == subCategoryId);

            //var courses = _context.Courses.Where(c=>c.CategoryId==categoryId);
            if (courses == null)
            {
                return NotFound();
            }

            ViewBag.categoryName = _categoryRepository.GetById(categoryId).CategoryName;
            ViewBag.subCategories = _categoryRepository.GetSubCategories(categoryId);
            //var categoryViewModel = new CategoryViewModel()
            //{
            //    categoryName = _categoryRepository.GetById(categoryId).CategoryName,
            //    courses = courses,
            //    subCategories = _categoryRepository.GetSubCategories(categoryId)


            //};



            return View(nameof(Details),await PaginatedList<Course>.CreateAsync(courses.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(categoryViewModel);
        }
    }
}
