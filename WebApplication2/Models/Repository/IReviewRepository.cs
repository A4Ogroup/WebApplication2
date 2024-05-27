namespace WebApplication2.Models.Repository
{
    public interface IReviewRepository
    {
        Review GetById(int id);
        IEnumerable<Review> GetAll();
        Review Add(Review review);
        Review Update(Review review);
        Review Delete(int id);
        bool Save();
    }
}