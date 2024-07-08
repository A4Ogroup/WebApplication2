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

        public void Delete(int id)
        {
            Review review=GetById(id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
           
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

        IEnumerable<Review> IReviewRepository.GetAllWithCourse()
        {
            return _context.Reviews.Include(r => r.Course);
        }

        IEnumerable<Review> IReviewRepository.GetTopReviews()
        {
            return _context.Reviews.OrderByDescending(c => c.Rate).Where(c => c.Status == true).Take(10).ToList();
        }
    }
}