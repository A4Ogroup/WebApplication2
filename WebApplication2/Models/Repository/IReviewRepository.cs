namespace WebApplication2.Models.Repository
{
    public interface IReviewRepository
    {
        Review GetById(int id);
        IEnumerable<Review> GetAll();
        IEnumerable<Review> GetAllWithCourse();
        Review Add(Review review);
        Review Update(Review review);
        void Delete(int id);
        bool Save();
    }
}