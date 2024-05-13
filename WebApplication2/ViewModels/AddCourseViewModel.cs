using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Helpers.Enums;

namespace WebApplication2.ViewModels
{
    public class AddCourseViewModel
    {
       // public int CourseId { get; set; }
        public string? Title { get; set; }

        
        public string? AddedByUserId { get; set; } //= Guid.NewGuid().ToString();
        
        public string? CourseDescription { get; set; }
        public string? TopicsCovered { get; set; }
        [DisplayName("Course Category")]
        public byte? CategoryId { get; set; }
        [DisplayName("Course SubCategory")]
        public int? SubcategoryId { get; set; }
        public string? Platform { get; set; }
        public int? LanguageId { get; set; }
        
        public Level? Level { get; set; }
        public bool? PriceStatus { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? AddingDate { get; set; } = DateTime.Now;
        public IFormFile? Picture { get; set; }
        public int? AverageRating { get; set; } = 0;
        public bool? Claimed { get; set; }
        public bool? Status { get; set; }
        //public string InstructorId { get; set; }
        //public string InstructorFullName { get; set; }
        [DisplayName("Average Video Length")]
        public VideoLengthCategory? VedioLength { get; set; }
        [DisplayName("Course Total Hours")]
        public int? CourseDuration { get; set; }
        public string? Link { get; set; }
    }
}
