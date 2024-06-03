using WebApplication2.Helpers.Enums;
using WebApplication2.Models;
namespace WebApplication2.ViewModels
{
    public class SearchViewModel
    {
        public SearchViewModel() { }

       public IEnumerable<Course> courses {  get; set; }= new List<Course>();
       public IEnumerable<Category> categories { get; set; } = new List<Category>();
       public IEnumerable<Language> languages { get; set; }=new List<Language>();
       public IEnumerable<Level> levels { get; set; } = new List<Level>();
    }
}
