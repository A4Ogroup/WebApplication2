﻿using WebApplication2.Models;
namespace WebApplication2.ViewModels
{
    public class ReviewDetailsViewModel
    {
        public int ReviewId { get; set; }
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