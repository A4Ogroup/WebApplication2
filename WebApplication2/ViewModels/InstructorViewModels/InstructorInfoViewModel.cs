using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Helpers.Enums;

namespace WebApplication2.ViewModels.InstructorViewModels
{
    public class InstructorInfoViewModel
    {

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]

        public string LastName { get; set; }
        [Required (ErrorMessage="Phone number is required.")]
        public string PhoneNumber { get; set; }

        
        public Gender Gender { get; set; }
        //[RegularExpression(@".*\.(jpg|png)$", ErrorMessage = "Picture must be jpg or png")]
        //[RegularExpression(@"\w+\.(jpg|png)",ErrorMessage ="Picture must be JPG or PNG")]
       // [imageFile]
        public IFormFile? Picture { get; set; }
    }
}
