using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable <Course> Course { get; set; }
        public IEnumerable<Review> Review { get; set; }
        public IEnumerable<Course> latestCourses { get; set; }
    }
}
