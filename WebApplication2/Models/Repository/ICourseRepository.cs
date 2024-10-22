﻿using Microsoft.EntityFrameworkCore;
using WebApplication2.ResourceParameters;
using WebApplication2.ViewModels;

namespace WebApplication2.Models.Repository
{
    public interface ICourseRepository
    {
        Course GetById(int id);
        
        IEnumerable<Course> GetAll();
        Task<IEnumerable<Course>> GetAllAsync();

        public IQueryable<Course> FilterCourses(CourseFilterViewModel filters,IQueryable<Course> resultCourses);
        public IEnumerable<Course> GetCoursesByCategory(int categoryId);
        public IEnumerable<Course> GetCourses(CourseResourceParameters? ResourceParameters);
        public IQueryable<Course> GetCourses(string? searchParam ,int categoryId);
        IEnumerable<Course> GetAllWithLanguage();
        IEnumerable<Course> GetAllWithLanguageAddedByInstructor(string id);
        IEnumerable<Course> GetTopCourses();
        IEnumerable<Course> GetLatestCourses();
        Course Add(Course course);
        Course Update(Course courseChanges);
        void Delete(int id);
        bool Save();

        
    }
}
