namespace WebApplication2.Models.Repository
{
    public interface ICourseRepository
    {
        Course GetById(int id);
        
        IEnumerable<Course> GetAll();
        Course Add(Course course);
        Course Update(int id, Course courseChanges);
        Course Delete(int id);
    }
}
