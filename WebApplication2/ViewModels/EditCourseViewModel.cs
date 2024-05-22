using WebApplication2.Helpers.Enums;
namespace WebApplication2.ViewModels
{
    public class EditCourseViewModel:AddCourseViewModel
    {

            public int CourseId { get; set; }
            public string? ExistingPhotoPath { get; set; }

       
    }
}
