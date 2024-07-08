using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Helpers
{
    public class FilterRequest
    {
        public CourseFilterViewModel Filters { get; set; }
        public int PageNumber { get; set; }

        //public IQueryable<Course> Courses { get; set; }
    }
}
