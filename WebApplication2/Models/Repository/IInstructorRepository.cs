using WebApplication2.ViewModels;

namespace WebApplication2.Models.Repository
{
    public interface IInstructorRepository
    {

        IEnumerable<Instructor> GetAll();
        Task<IEnumerable<Instructor>> GetAllAsync();

        IEnumerable<Instructor> GetAllWithCourses();
        Instructor GetById(string id);
        public bool IsInstructorVerified(string instructorId);
        Instructor Add(Instructor instructor);
        Instructor Update(Instructor instructor);
        void Delete(string id);
         bool Save();
    }
}
