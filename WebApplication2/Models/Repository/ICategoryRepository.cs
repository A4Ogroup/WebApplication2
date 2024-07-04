namespace WebApplication2.Models.Repository
{
    public interface ICategoryRepository
    {
        public Category GetById(int categoryId);
        public IEnumerable<Category> GetAll();
        public IEnumerable<SubCategory> GetSubCategories(int categoryId);
        public bool CategoryExists(int categoryId);
        public bool SubCategoryExists(int subCategoryId);
    }
}
