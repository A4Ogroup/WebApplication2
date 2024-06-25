using static WebApplication2.ViewModels.PagenationViewModel; // Ensure you have the PagedList package installed
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class CourseDetailsViewModel
    {
        public Course Course { get; set; }
        public PaginatedList<Review> PaginatedReviews { get; set; }
    }
}

