using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable <Course> Course { get; set; }
        public IEnumerable<Review> Review { get; set; }
        public IEnumerable<Report> reports { get; set; }
        public IEnumerable<Instructor> instructors { get; set; }
        public IEnumerable<Student> students { get; set; }

        public IEnumerable<Course> latestCourses { get; set; }
        public IEnumerable<CourseClaimReport> CourseClaimReports { get; set; }
    }
}
