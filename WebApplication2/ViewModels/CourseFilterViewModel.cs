using WebApplication2.Helpers.Enums;
using WebApplication2.Models;
using System.Collections.Generic;
using WebApplication2.ResourceParameters;

namespace WebApplication2.ViewModels
{
    public class CourseFilterViewModel
    {
        //public CourseResourceParameters searchParameters { get; set; }
        public List<double>? Ratings { get; set; }
        public List<byte>? CategoryIds { get; set; }
        public List<int>? LanguageIds { get; set; }
        public List<Level>? Levels { get; set; }
        public List<VideoLengthCategory>? VideoLengths { get; set; }
        public List<bool>? IsFree { get; set; }
       // public IEnumerable<Course> Courses { get; set; }
    }
}

