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
            return _context.Courses.FirstOrDefault(C=>C.CourseId==id);
        }
        public IEnumerable<Course> GetAll()
        {
            return _context.Courses;
        }
        public Course Add(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course;
        }

        public Course Delete(int id)
        {
            throw new NotImplementedException();
        }

       

        public Course Update(int id, Course courseChanges)
        {
            throw new NotImplementedException();
        }
    }
}
