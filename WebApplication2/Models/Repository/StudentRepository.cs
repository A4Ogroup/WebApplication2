
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models.Repository
{
    public class StudentRepository : IStudentRepository
    {
        LconsultDBContext _context;
        public StudentRepository(LconsultDBContext context)
        {
            _context = context;
        }
        public Student Add(Student student)
        {
            _context.Students.Add(student);
            //_context.SaveChanges();
            return student;
        }

        public void Delete(string id)
        {
           var student = _context.Students.Include(i => i.StudentNavigation).FirstOrDefault(s => s.StudentId == id);
            _context.SaveChanges();
            _context.Students.Remove(student);
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.Include(i => i.StudentNavigation);
        }

        //public IEnumerable<Student> GetAllWithCourses()
        //{
        //    return _context.Students.Include(s => s.c)
        //}

        public IEnumerable<Student> GetAllWithReportss()
        {
            return _context.Students.Include(s => s.Reports);
        }

        public IEnumerable<Student> GetAllWithReviews()
        {
            return _context.Students.Include(s => s.Reviews);
        }

        public Student GetById(string id)
        {
            return _context.Students.Include(i => i.StudentNavigation).FirstOrDefault(s => s.StudentId == id);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Student Update(Student student)
        {
            var _student= _context.Students.Attach(student);
            _student.State=EntityState.Modified;
            return student;
        }

        async Task<IEnumerable<Student>> IStudentRepository.GetAllAsync()
        {
            return await _context.Students.Include(i => i.StudentNavigation).ToListAsync();
        }
    }
}
