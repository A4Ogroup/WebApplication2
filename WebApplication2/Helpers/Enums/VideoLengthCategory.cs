using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Helpers.Enums
{

    public enum VideoLengthCategory
    {
        [Display(Name = "Short (under 5 minutes)")]
        Short = 1,

        [Display(Name = "Medium (5 to 15 minutes)")]
        Medium = 2,

        [Display(Name = "Long (15 to 30 minutes)")]
        Long = 3,

        [Display(Name = "Extended (over 30 minutes)")] 
        Extended = 4  // Use sparingly for complex, detailed content or workshops
    }

}

