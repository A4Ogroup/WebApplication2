using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class AddReviewViewModel
    {
        [Required(ErrorMessage = "Description fieled is required")]
        public string Descritipn { get; set; }
        public bool? Status { get; set; }
        [Required(ErrorMessage = "Rate fieled is required")]
        public byte Rate { get; set; }
        public DateTime RatingDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Material Quality fieled is required")]
        public int MaterialQuality { get; set; }
        [Required(ErrorMessage = "Support Quality fieled is required")]
        public int SupportQuality { get; set; }
        [Required(ErrorMessage = "Engagement Level fieled is required")]
        public int EngagementLevel { get; set; }
        [Required(ErrorMessage = "Technical Quality fieled is required")]
        public int TechnicalQuality { get; set; }
        [Required(ErrorMessage = "Content Quality fieled is required")]
        public int ContentQuality { get; set; }
        [Required(ErrorMessage = "Over All Satisfication fieled is required")]
        public int OverAllSatisfication { get; set; }
        public int CourseId { get; set; }
        public string StudentId { get; set; }

        public string GetFormattedDate(DateTime date)
        {
            var time = DateTime.UtcNow - date;
            double delta = Math.Abs(time.TotalSeconds);

            if (delta < 1)
            {
                return "just now";
            }
            else if (delta < 60)
            {
                return time.Seconds <= 1
                        ? "1 second ago"
                        : $"{time.Seconds} seconds ago";
            }
            else if (delta < 3600)
            {
                int minutes = (int)(time.TotalMinutes);
                return minutes <= 1
                        ? "1 minute ago"
                        : $"{minutes} minutes ago";
            }
            else if (delta < 86400)
            {
                int hours = (int)(time.TotalHours);
                return hours <= 1
                        ? "1 hour ago"
                        : $"{hours} hours ago";
            }
            else if (delta < 2592000) // One month (30 days)
            {
                int days = (int)(time.TotalDays);
                return days <= 1
                        ? "1 day ago"
                        : $"{days} days ago";
            }
            else if (delta < 31536000) // One year (365 days)
            {
                int months = (int)(time.TotalDays / 30);
                return months <= 1
                        ? "1 month ago"
                        : $"{months} months ago";
            }
            else
            {
                int years = (int)(time.TotalDays / 365);
                return years <= 1
                        ? "1 year ago"
                        : $"{years} years ago";
            }
        }
    }
}