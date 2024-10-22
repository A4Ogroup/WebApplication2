﻿namespace WebApplication2.Models.Repository
{
    public interface IReviewRepository
    {
        Review GetById(int id);
        IEnumerable<Review> GetAll();
        Task<IEnumerable<Review>> GetAllAsync();

        IEnumerable<Review> GetAllWithCourse();
        IEnumerable<Review> GetTopReviews();
        Review Add(Review review);
        Review Update(Review review);
        void Delete(int id);
        bool Save();
    }
}