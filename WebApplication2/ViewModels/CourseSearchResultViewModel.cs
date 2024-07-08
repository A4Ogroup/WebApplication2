using WebApplication2.Helpers.Enums;

namespace WebApplication2.ViewModels
{
    public class CourseSearchResultViewModel
    {
        public string? Title { get; set; }
        public int CourseId { get; set; }
        public string? CourseDescription { get; set; }
        public string? TopicsCovered { get; set; }

        public int? LanguageId { get; set; }

        public Level? Level { get; set; }
        public bool? PriceStatus { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? Picture { get; set; }
        public double? AverageRating { get; set; } = 0.0;
        public bool? Claimed { get; set; }
        public bool Status { get; set; }
        public string? InstructorId { get; set; }
        public string? InstructorFullName { get; set; }
        // [Required]
        public VideoLengthCategory? VedioLength { get; set; }
        public int? CourseDuration { get; set; }
        public string? Link { get; set; }

    }
}
