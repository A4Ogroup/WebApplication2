using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebApplication2.Models.Repository
{
   
    public class CourseRepository : ICourseRepository
    {
        LconsultDBContext _context;
        public CourseRepository(LconsultDBContext context)
        {
           _context = context;
        }
        public Course GetById(int id)
        {
            return _context.Courses.FirstOrDefault(C => C.CourseId == id);
        }
        public IEnumerable<Course> GetAll()
        {
            return _context.Courses;
        }
        public Course Add(Course course)
        {
            if(course is  null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course;
        }

        public Course Delete(int id)
        {
            throw new NotImplementedException();
        }

       

        public Course Update( Course courseChanges)
        {
            var course =_context.Courses.Attach(courseChanges);
            course.State = EntityState.Modified;
            return courseChanges;
            
        }


        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
