using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Helpers.Enums;
using WebApplication2.ViewModels.InstructorViewModels;

namespace WebApplication2
{
    public class StudentInfoViewModel
    {

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]


        public Gender Gender { get; set; }
        //[RegularExpression(@".*\.(jpg|png)$", ErrorMessage = "Picture must be jpg or png")]
        //[RegularExpression(@"\w+\.(jpg|png)",ErrorMessage ="Picture must be JPG or PNG")]
        // [imageFile]
        public IFormFile? Picture { get; set; }
        public int CategoryId { get; set; }

    }
}
