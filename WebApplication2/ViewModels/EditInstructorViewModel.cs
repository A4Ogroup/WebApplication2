using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Helpers.Enums;

namespace WebApplication2.ViewModels
{
    public class EditInstructorViewModel
    {
        public string InstructorId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Write Email correctly")]
        [Remote(action: "VerifyEmail", controller: "Instructor", AdditionalFields = "OriginalEmail")]
        public string Email { get; set; }
        public string OriginalEmail { get; set; }


        [Remote(action: "VerifyUserName", controller: "Instructor", AdditionalFields = "OriginalUserName")]
        public string UserName { get; set; }
        public string OriginalUserName { get; set; }





        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public IFormFile? Picture { get; set; }

        public string? PhoneNumber { get; set; }

        public Gender Gender { get; set; }


        public string Profession { get; set; }
        public byte? YearsExperince { get; set; }
        public string? About { get; set; }
        public string Website { get; set; }
        public List<string>? SocialMediaAccounts { get; set; }
        public string? ExistingPhotoPath { get; set; }





    }
}
