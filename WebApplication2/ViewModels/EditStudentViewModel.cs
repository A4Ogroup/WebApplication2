using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Helpers.Enums;

namespace WebApplication2.ViewModels
{
    public class EditStudentViewModel
    {
        public string StudentId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Write Email correctly")]
        [Remote(action: "VerifyEmail", controller: "Instructor", AdditionalFields = "OriginalEmail")]
        public string Email { get; set; }
        public string OriginalEmail { get; set; }


        [Remote("IsUserNameAlreadyExists", "Account", ErrorMessage = "User Name already exist")]
        public string UserName { get; set; }







        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public IFormFile? Picture { get; set; }

        public Gender Gender { get; set; }
        public List<int>? SelectedTagIds { get; set; }
    }
}
