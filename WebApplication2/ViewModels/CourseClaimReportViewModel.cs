using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class CourseClaimReportViewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string InstructorId { get; set; }
        public string Reason { get; set; }
        public DateTime SubmittedDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        public Course Course { get; set; }
        public User Instructor { get; set; }
    }
}
