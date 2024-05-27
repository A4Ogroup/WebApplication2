using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models.Repository
{
    public class CategoryRepositroy:ICategoryRepository
    {
        private readonly LconsultDBContext _context;
        public CategoryRepositroy(LconsultDBContext context)
        {
            _context = context;    
        }
        public IEnumerable<Category> GetAll()
        {
return _context.Categories; 
        }

        public Category GetById(int categoryId) {

            return _context.Categories.FirstOrDefault();
        }
        public IEnumerable<SubCategory> GetSubCategories (int categoryId)
        {

            return _context.SubCategories.Where(S=>S.CategoryId==categoryId);
        }


        public bool CategoryExists(int  categoryId)
        {
            if (categoryId == 0)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }

            return _context.Categories.Any(c => c.CategoryId == categoryId);
        }
    }
}
