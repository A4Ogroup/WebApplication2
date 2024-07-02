using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApplication2.ResourceParameters;
using WebApplication2.ViewModels;

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
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id));    
            }
            return _context.Courses.FirstOrDefault(C => C.CourseId == id);
        }
        public IEnumerable<Course> GetAll()
        {
            return _context.Courses;
        }

        public IEnumerable<Course> GetCoursesByCategory(int categoryId)
        {
            if(categoryId==0)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }


            return _context.Courses.Where(C=>C.CategoryId==categoryId);
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


        public void Delete(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var course = _context.Courses.FirstOrDefault(C => C.CourseId == id);
            _context.SaveChanges();
            _context.Courses.Remove(course);
        }

       

        public Course Update( Course courseChanges)
        {
            var course =_context.Courses.Attach(courseChanges);
            course.State = EntityState.Modified;
            return courseChanges;
            
        }
        public  IEnumerable<Course> GetCourses(CourseResourceParameters? ResourceParameters)
        {
            if (ResourceParameters == null)
            {
                return GetAll();
            }
            var collection = _context.Courses as IQueryable<Course>;
            if (ResourceParameters.CategoryId !=null)
            {

                //ResourceParameters.Category = ResourceParameters.Category.Trim();
                collection = collection.Where(a => a.CategoryId == ResourceParameters.CategoryId);

            }
            if (!string.IsNullOrWhiteSpace(ResourceParameters.SearchQuery))
            {

                ResourceParameters.SearchQuery =  ResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.Title.Contains(ResourceParameters.SearchQuery)
                || a.InstructorFullName.Contains(ResourceParameters.SearchQuery)
                || a.CourseDescription.Contains(ResourceParameters.SearchQuery));

            }
            return collection.ToList();


        }

        public IQueryable<Course> FilterCourses(CourseFilterViewModel filters,IEnumerable<Course> resultCourses)
        {

            var filteredCourses = resultCourses as IQueryable<Course>;


            if (filters.IsFree != null)
            {
                if (filters?.IsFree?.Count() == 1)
                    filteredCourses = filteredCourses.Where(c => c.PriceStatus == filters.IsFree[0]);


            }

            //if (filters.IsFree != null && filters.IsFree.Contains(false))
            //{
            //    filteredCourses = filteredCourses.Where(c => c.PriceStatus == false);
            //}
            if (filters.Ratings != null && filters.Ratings.Any())
            {
                filteredCourses = filteredCourses.Where(c => filters.Ratings.Any(r => r <= c.AverageRating));
            }

            if (filters.CategoryIds != null && filters.CategoryIds.Any())
            {
                filteredCourses = filteredCourses.Where(c => filters.CategoryIds.Contains(c.CategoryId ?? 0));
            }

            if (filters.LanguageIds != null && filters.LanguageIds.Any())
            {
                filteredCourses = filteredCourses.Where(c => filters.LanguageIds.Contains(c.LanguageId ?? 0));
            }

            if (filters.Levels != null && filters.Levels.Any())
            {
                filteredCourses = filteredCourses.Where(c => filters.Levels.Contains(c.Level ?? 0));
            }

            if (filters.VideoLengths != null && filters.VideoLengths.Any())
            {
                filteredCourses = filteredCourses.Where(c => filters.VideoLengths.Contains(c.VedioLength ?? 0));
            }

            return filteredCourses;
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
