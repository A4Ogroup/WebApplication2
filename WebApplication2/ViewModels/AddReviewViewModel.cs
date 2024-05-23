using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class AddReviewViewModel
    {
        public int ReviewId { get; set; }
        public string Descritipn { get; set; }
        public bool? Status { get; set; }
        public byte? Rate { get; set; }
        public DateTime? RatingDate { get; set; } = DateTime.Now;
    }
}