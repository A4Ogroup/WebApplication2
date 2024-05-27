using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            
        }
        public string categoryName;
        public   IEnumerable<SubCategory> subCategories { get; set; }= new List<SubCategory>();
        public   IEnumerable<Course> courses { get; set; }= new List<Course>();
        


    }
}
