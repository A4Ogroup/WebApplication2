using Microsoft.EntityFrameworkCore;
using WebApplication2.ViewModels;

namespace WebApplication2.Models.Repository
{
    public interface ICourseRepository
    {
        Course GetById(int id);
        
        IEnumerable<Course> GetAll();
        public IQueryable<Course> FilterCourses(CourseFilterViewModel filters,IEnumerable<Course> resultCourses);
        public IEnumerable<Course> GetCoursesByCategory(int categoryId);
        Course Add(Course course);
        Course Update(Course courseChanges);
        void Delete(Course course);
        bool Save();

        
    }
}
