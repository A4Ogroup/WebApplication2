
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        LconsultDBContext _context;
        public Instructor Add(Instructor instructor)
        {
            _context.Instructors.Add(instructor);
            _context.SaveChanges();
            return instructor;
        }

        public void Delete(string id)
        {

            var instructor = _context.Instructors.FirstOrDefault(i => i.InstructorId == id);
            _context.SaveChanges();
            _context.Instructors.Remove(instructor);


        }

        public IEnumerable<Instructor> GetAll()
        {
            return _context.Instructors;
        }

        public IEnumerable<Instructor> GetAllWithCourses()
        {
            return _context.Instructors.Include(i => i.Courses);
        }

        public Instructor GetById(string id)
        {
            return _context.Instructors.FirstOrDefault(i => i.InstructorId == id);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Instructor Update(Instructor instructor)
        {
            var _instructor = _context.Instructors.Attach(instructor);
            _instructor.State= EntityState.Modified;
            return instructor;
        }
    }
}
