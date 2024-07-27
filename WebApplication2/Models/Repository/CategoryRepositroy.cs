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

        public Category GetById(byte categoryId) {

            return _context.Categories.Find(categoryId);
        }
        public IEnumerable<SubCategory> GetSubCategories (int categoryId)
        {

            return _context.SubCategories.Where(S=>S.CategoryId==categoryId);
        }


        public bool CategoryExists(int categoryId)
        {
            //if (categoryId == 0)
            //{
            //    throw new ArgumentNullException(nameof(categoryId));
            //}

            return _context.Categories.Any(c => c.CategoryId == categoryId);
        }

            public  bool SubCategoryExists(int subCategoryId)
            {
                //if (subCategoryId == 0)
                //{
                //    throw new ArgumentNullException(nameof(subCategoryId));
                //}

                return  _context.SubCategories.Any(c => c.SubId == subCategoryId);
            }
        }
}
