﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Helpers.Enums;

namespace WebApplication2
{
    public class StudentRegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        
        [EmailAddress(ErrorMessage = "Write Email correctly")]
        [Remote("IsEmailAlreadyRegistered", "account", ErrorMessage = "Email already exist")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password does not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public IFormFile? Picture { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public Gender Gender { get; set; }



      public List<int>? SelectedTagIds { get; set; }


    }
}
