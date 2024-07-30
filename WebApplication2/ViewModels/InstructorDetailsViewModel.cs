using WebApplication2.Helpers.Enums;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class InstructorDetailsViewModel
    {
        public string InstructorId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Picture { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public Gender Gender { get; set; }
        public string Profession { get; set; }
        public byte? YearsExperince { get; set; }
        public string About { get; set; }
        public string Website { get; set; }

        public IEnumerable<SocialMediaAccount> SocialMediaAccounts
        {
            get; set;
        }

        public PaginatedListNew<Course> Course { get; set; }
        public IEnumerable<Course> Courses { get; set; }

  
        
    }
}
