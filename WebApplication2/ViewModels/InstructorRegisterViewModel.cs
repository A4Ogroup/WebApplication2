using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class InstructorRegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public  string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public  string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public  string ConfirmPassword { get; set; }
        public string? Pic { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public  string? PhoneNumber { get; set; }
        
        public string? CountryCode { get; set; }
        public string? Profession { get; set; }
        public byte? YearsExperince { get; set; }
        public string? About { get; set; }

    }
}
