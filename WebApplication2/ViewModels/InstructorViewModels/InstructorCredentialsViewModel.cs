using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels.InstructorViewModels
{
    public class InstructorCredentialsViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Remote("IsEmailAlreadyRegistered", "Account", ErrorMessage = "Email already exist")]
        public string Email { get; set; }
        [Required]
        [Remote("IsUserNameAlreadyExists", "Account", ErrorMessage = "User Name already exist")]

        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
