using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Helpers.Enums;

namespace WebApplication2.ViewModels
{
    public class InstructorRegisterViewModel
    {

        #region instructorCredentials
        [Required]
        [EmailAddress(ErrorMessage = "Write Email correctly")]
        [Remote("IsEmailAlreadyRegistered", "account", ErrorMessage = "Email already exist")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }

        #endregion




        #region InstructorInfo


        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public IFormFile? Picture { get; set; }
        
        public  string? PhoneNumber { get; set; }
        
        public Gender Gender { get; set; }

        #endregion





        #region instructorProfession

        public string Profession { get; set; }
        public byte YearsExperince { get; set; }
        public string? About { get; set; }
        public string  Website { get; set; }
        public List<string>? SocialMediaAccounts { get; set; } = new List<string>();



        #endregion
    }

}
