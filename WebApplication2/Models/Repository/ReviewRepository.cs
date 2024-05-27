using Microsoft.EntityFrameworkCore;

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

        public Review Update(Review reviewchanges)
        {
            var _review = _context.Reviews.Attach(reviewchanges);
            _review.State= EntityState.Modified;
            return reviewchanges;
        }
        public bool Save()
        {
            return (_context.SaveChanges() >=0);
        }
    }
}