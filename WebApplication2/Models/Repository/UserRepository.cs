
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        LconsultDBContext _context;
        public UserRepository(LconsultDBContext context) {
         _context = context;
        }

        
        public User Add(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
