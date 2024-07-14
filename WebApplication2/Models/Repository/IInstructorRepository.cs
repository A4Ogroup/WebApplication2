using WebApplication2.ViewModels;

namespace WebApplication2.Models.Repository
{
    public interface IInstructorRepository
    {

        IEnumerable<Instructor> GetAll();
        IEnumerable<Instructor> GetAllWithCourses();
        Instructor GetById(string id);
        Instructor Add(Instructor instructor);
        Instructor Update(Instructor instructor);
        void Delete(string id);
         bool Save();
    }
}
