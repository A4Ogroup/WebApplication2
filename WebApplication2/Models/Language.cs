﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Language
    {
        public Language()
        {
            Courses = new HashSet<Course>();
        }

        public int LanguageId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}