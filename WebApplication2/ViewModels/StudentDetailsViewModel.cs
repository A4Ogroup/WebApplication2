using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Helpers.Enums;
using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
    public class StudentDetailsViewModel
    {
        public string StudentId {  get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile? Picture { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public Gender Gender { get; set; }
        public List<int>? SelectedTagIds { get; set; }
        public Review review { get; set; }
        public PaginatedListNew<Review> Reviews { get; set; }



    }
}
