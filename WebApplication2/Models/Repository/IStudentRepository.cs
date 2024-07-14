using WebApplication2.ViewModels;

namespace WebApplication2.Models.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        IEnumerable<Student> GetAllWithReviews();
        //IEnumerable<Student> GetAllWithCourses();
        IEnumerable<Student> GetAllWithReportss();
        Student GetById(string id);
        Student Add(Student student);
        Student Update(Student student);
        void Delete(string id);
        bool Save();


    }
}
