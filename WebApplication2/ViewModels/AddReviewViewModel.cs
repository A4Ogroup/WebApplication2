using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class AddReviewViewModel
    {
       
        public string Descritipn { get; set; }
        public bool? Status { get; set; }
        public byte? Rate { get; set; }
        public DateTime? RatingDate { get; set; } = DateTime.Now;
        public int MaterialQuality { get; set; }
        public int SupportQuality { get; set; }
        public int EngagementLevel { get; set; }
        public int TechnicalQuality { get; set; }
        public int ContentQuality { get; set; }
        public int OverAllSatisfication { get; set; }
        public int CourseId { get; set; }
    }
}