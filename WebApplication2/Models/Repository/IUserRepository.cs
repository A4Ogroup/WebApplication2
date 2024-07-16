namespace WebApplication2.Models.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(string id);
        User Add(User user);
        User Update(User user);
        void Delete(string id);
        bool Save();
    }
}
