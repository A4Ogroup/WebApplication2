namespace WebApplication2.Models.Repository
{
    public class ReviewRepository : IReviewRepository
    {

        LconsultDBContext _context;

        public ReviewRepository(LconsultDBContext context)
        {
            _context = context;
        }
        public Review Add(Review review)
        {
            _context.Add(review);
            _context.SaveChanges();
            return review;
        }

        public Review Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetAll()
        {
           return _context.Reviews;
        }

        public Review GetById(int id)
        {
           return _context.Reviews.FirstOrDefault(R => R.ReviewId == id); 
        }

        public Review Update(int id, Review review)
        {
            throw new NotImplementedException();
        }
    }
}