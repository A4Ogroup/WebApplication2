﻿namespace WebApplication2.Models.Repository
{
    public interface IReviewRepository
    {
        Review GetById(int id);
        IEnumerable<Review> GetAll();
        Review Add(Review review);
        Review Update(int id,Review review);
        Review Delete(int id);
    }
}