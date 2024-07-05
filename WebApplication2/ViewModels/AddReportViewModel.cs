using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;
namespace WebApplication2.ViewModels
{
    public class AddReportViewModel
    {
        [Required(ErrorMessage = "Message fieled is required")]
        public string Message { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;
        public int ReviewId { get; set; }
    }
}
