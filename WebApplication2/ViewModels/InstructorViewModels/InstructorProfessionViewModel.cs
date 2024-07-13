using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels.InstructorViewModels
{
    public class InstructorProfessionViewModel
    {
        [Required(ErrorMessage ="This Feild is Required.")]
        public string Profession { get; set; }
        public byte YearsExperince { get; set; }
        public string? About { get; set; }
        [RegularExpression(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$", ErrorMessage = "Please enter a valid URL.")]
        public string? Website { get; set; }

        //[RegularExpression(@"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$", ErrorMessage = "Please enter a valid URL.")]
        public List<string>? SocialMediaAccounts { get; set; } = new List<string>{"","",""};


    }
}
