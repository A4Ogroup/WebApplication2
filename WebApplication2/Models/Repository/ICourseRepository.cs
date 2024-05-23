using WebApplication2.ViewModels;

namespace WebApplication2.Models.Repository
{
    public interface ICourseRepository
    {
        Course GetById(int id);
        
        IEnumerable<Course> GetAll();
        Course Add(Course course);
        Course Update( Course courseChanges);
        Course Delete(int id);
        bool Save();
    }
}
