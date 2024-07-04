using WebApplication2.Models;
namespace WebApplication2.ViewModels
{
    public class AddReportViewModel
    {
        public string Message { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;
        public int ReviewId { get; set; }
    }
}
