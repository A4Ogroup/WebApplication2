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
        public IEnumerable<Course> GetAllExcepted()
        {
            return _context.Courses.Where(c=>c.Status==true);
        }



        public IEnumerable<Course> GetCoursesByCategory(int categoryId)
        {
            if(categoryId==0)
            {
                throw new ArgumentNullException(nameof(categoryId));
            }


            return _context.Courses.Where(C=>C.CategoryId==categoryId);
        }

        public IEnumerable<Course> GetAllWithLanguage()
        {
            return _context.Courses.Where(c=>c.Status==true).Include(c => c.Language);
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
                return GetAllExcepted();
            }
            var collection = _context.Courses as IQueryable<Course>;
            collection = collection.Where(c => c.Status == true);
            if (ResourceParameters.CategoryId !=null && ResourceParameters.CategoryId!=0 )
            {

                //ResourceParameters.Category = ResourceParameters.Category.Trim();
                collection = collection.Where(a => a.CategoryId == ResourceParameters.CategoryId);

            }
            if (!string.IsNullOrWhiteSpace(ResourceParameters.SearchQuery))
            {

                ResourceParameters.SearchQuery =  ResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.Title.Contains(ResourceParameters.SearchQuery)
                || a.InstructorFullName.Contains(ResourceParameters.SearchQuery)
                || a.CourseDescription.Contains(ResourceParameters.SearchQuery)|| a.TopicsCovered.Contains(ResourceParameters.SearchQuery));

            }
            collection = collection.Include(c => c.Reviews);
            
            return collection.AsEnumerable();


        }
        public  IQueryable<Course> GetCourses(string? searchParam, int categoryId)
        {
            if (searchParam == null)
            {
                return _context.Courses;
            }
            var collection = _context.Courses.AsQueryable();
            collection = collection.Where(c => c.Status == true);

            if (categoryId !=null && categoryId!=0 )
            {

                //ResourceParameters.Category = ResourceParameters.Category.Trim();
                collection = collection.Where(a => a.CategoryId ==categoryId);

            }
            if (!string.IsNullOrWhiteSpace(searchParam))
            {

                searchParam =  searchParam.Trim();
                collection = collection.Where(a => a.Title.Contains(searchParam)
                || a.InstructorFullName.Contains(searchParam)
                || a.CourseDescription.Contains(searchParam)|| a.TopicsCovered.Contains(searchParam));

            }
            collection = collection.Include(c => c.Reviews);

            return collection;


        }

        public IQueryable<Course> FilterCourses(CourseFilterViewModel filters,IQueryable<Course> resultCourses)
        {

            var filteredCourses = resultCourses;


            if (filters.IsFree != null)
            {
                if (filters?.IsFree?.Count() == 1)
                    filteredCourses = filteredCourses.Where(c => c.PriceStatus == filters.IsFree[0]);


            }

            //if (filters.IsFree != null && filters.IsFree.Contains(false))
            //{
            //    filteredCourses = filteredCourses.Where(c => c.PriceStatus == false);
            //}
            //if (filters.Ratings != null && filters.Ratings.Any())
            //{
            //    filteredCourses = filteredCourses.Where(c => filters.Ratings.Any(r => r <= c.AverageRating));
            //}

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

        IEnumerable<Course> ICourseRepository.GetTopCourses()
        {
            return GetAllExcepted().OrderByDescending(c => c.AverageRating).Where(c => c.Status == true).Take(10).ToList();
        }

        IEnumerable<Course> ICourseRepository.GetLatestCourses()
        {
           return GetAllExcepted().OrderByDescending(c => c.AddingDate).Where(c => c.Status == true).Take(10).ToList();
        }

        IEnumerable<Course> ICourseRepository.GetAllWithLanguageAddedByInstructor(string id)
        {
            return _context.Courses.Include(c => c.Language).Where(c => c.InstructorId == id);
        }
    }
}
