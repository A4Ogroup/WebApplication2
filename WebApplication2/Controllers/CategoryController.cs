using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Repository;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ILogger<CategoryController> logger, ICourseRepository courseRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
        }



        public IActionResult Index()
        {
            return View("Category");
        }


        public IActionResult Details(int categoryId) 
        { 

            if (!_categoryRepository.CategoryExists(categoryId)) 
            {

             return NotFound();
               
            }

            var courses = _courseRepository.GetCoursesByCategory(categoryId);
            if (courses == null)
            {
                return NotFound();
            }

            var categoryViewModel = new CategoryViewModel()
            {
                categoryName = _categoryRepository.GetById(categoryId).CategoryName,
                courses = courses,
                subCategories = _categoryRepository.GetSubCategories(categoryId)


            };



            return View( categoryViewModel);
        }
    }
}
