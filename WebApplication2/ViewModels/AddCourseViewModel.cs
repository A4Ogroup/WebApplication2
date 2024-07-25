using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WebApplication2.Helpers.Enums;

namespace WebApplication2.ViewModels
{
    public class AddCourseViewModel
    {
        // public int CourseId { get; set; }
        //[Required(ErrorMessage = "Course Name field is required")]
        //[RegularExpression(@"^[^;'\-\-\/*<>\(\)]*$", ErrorMessage = "Input contains invalid characters.")]
        public string Title { get; set; }
        public string? AddedByUserId { get; set; } //= Guid.NewGuid().ToString();
        [Required(ErrorMessage = "Course Description field is required")]
        [MinLength(20,ErrorMessage ="The description is less than 20 lettre.")]
        [MaxLength(1000)]
        public string CourseDescription { get; set; }
        [Required(ErrorMessage = "Topics Covered field is required")]
        public string TopicsCovered { get; set; }
        [DisplayName("Course Category")]
        [Required(ErrorMessage = "Catgory field is required")]
        public byte CategoryId { get; set; }
        [DisplayName("Course SubCategory")]
        public int? SubcategoryId { get; set; }
        [Required(ErrorMessage = "Platform field is required")]
        public string Platform { get; set; }
        public int LanguageId { get; set; }
        [Required(ErrorMessage = "Level field is required")]
        public Level Level { get; set; }
        [Required(ErrorMessage = "Price Status field is required")]
        public bool PriceStatus { get; set; }
        [Required(ErrorMessage = "Last Update field is required")]
        public DateTime LastUpdate { get; set; }
        public DateTime AddingDate { get; set; } = DateTime.Now;
        //[Required(ErrorMessage = "Picture field is required")]
        // [RegularExpression(@"^.*\.(jpg|jpeg|png|gif|bmp|tiff|webp)$", ErrorMessage = "Picture must be jpg or png")]
        //  [RegularExpression(@"\w+\.(jpg|png)",ErrorMessage ="Picture must be JPG or PNG and does not contain any spaces.")]
        [ImageFile]
        public IFormFile Picture { get; set; }
        public double? AverageRating { get; set; } = 0.0;
        public bool Claimed { get; set; }
        public bool Status { get; set; } = false;
        //public string InstructorId { get; set; }
        public string InstructorFullName { get; set; }
        public string? InstructorId{ get; set; }
        [DisplayName("Average Video Length")]
        [Required(ErrorMessage = "Vedio Length field is required")]
        public VideoLengthCategory? VedioLength { get; set; }
        [DisplayName("Course Total Hours")]
        [Required(ErrorMessage = "Course Duration field is required")]
        public int CourseDuration { get; set; }
        [RegularExpression(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$", ErrorMessage = "Please enter a valid URL.")]
        [Required(ErrorMessage = "Link field is required")]
        public string Link { get; set; }
    }
}
