﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication2.Models
{
    public partial class LconsultDBContext : DbContext
    {
        public LconsultDBContext()
        {
        }
        public LconsultDBContext(DbContextOptions<LconsultDBContext> options) :base(options) { }
        

       

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<SocialMediaAccount> SocialMediaAccounts { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<User> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
////            if (!optionsBuilder.IsConfigured)
////            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
////                optionsBuilder.UseSqlServer("Data Source=E-LENOVO-LAPTOP\\SQLEXPRESS;Initial Catalog=LconsultDBv2;Integrated Security=True");
////            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("categoryID");

                entity.Property(e => e.CategoryName).HasColumnName("categoryName");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseId).HasColumnName("courseID");

                entity.Property(e => e.AddedByUserId).HasMaxLength(400);

                entity.Property(e => e.AddingDate)
                    .HasColumnType("date")
                    .HasColumnName("addingDate");

                entity.Property(e => e.AverageRating).HasColumnName("averageRating");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.Claimed).HasColumnName("claimed");

                entity.Property(e => e.CourseDescription).HasColumnName("courseDescription");

                entity.Property(e => e.CourseDuration)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("courseDuration");

                entity.Property(e => e.InstructorFullName)
                    .HasMaxLength(50)
                    .HasColumnName("instructorFullName");

                entity.Property(e => e.InstructorId)
                    .HasMaxLength(400)
                    .HasColumnName("instructorID");

                entity.Property(e => e.LanguageId).HasColumnName("languageID");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("date")
                    .HasColumnName("lastUpdate");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.Platform)
                    .HasMaxLength(30)
                    .HasColumnName("platform");

                entity.Property(e => e.PriceStatus).HasColumnName("priceStatus");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubcategoryId).HasColumnName("subcategoryID");

                entity.Property(e => e.TopicsCovered).HasColumnName("topicsCovered");

                entity.Property(e => e.VedioLenght)
                    .HasMaxLength(10)
                    .HasColumnName("vedioLenght");

                entity.HasOne(d => d.AddedByUser)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.AddedByUserId);

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_Course_User_InstructroID");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_Course_Language");

                entity.HasOne(d => d.Subcategory)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubcategoryId)
                    .HasConstraintName("FK_Course_SubCategory");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("Instructor");

                entity.Property(e => e.InstructorId)
                    .HasMaxLength(400)
                    .HasColumnName("instructorID");

                entity.Property(e => e.About).HasColumnName("about");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(50)
                    .HasColumnName("countryCode");

                entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");

                entity.Property(e => e.Profession)
                    .HasMaxLength(50)
                    .HasColumnName("profession");

                entity.Property(e => e.YearsExperince).HasColumnName("yearsExperince");

                entity.HasOne(d => d.InstructorNavigation)
                    .WithOne(p => p.Instructor)
                    .HasForeignKey<Instructor>(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Instructor_User");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language");

                entity.Property(e => e.LanguageId).HasColumnName("languageID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId).HasColumnName("reportID");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.ReportDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewId).HasColumnName("reviewID");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(400)
                    .HasColumnName("studentID");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK_Report_Review");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Report_Student");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("reviewID");

                entity.Property(e => e.CourseId).HasColumnName("courseID");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.RatingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ratingDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(400)
                    .HasColumnName("studentID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Review_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Review_Student");
            });

            modelBuilder.Entity<SocialMediaAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("SocialMediaAccount");

                entity.Property(e => e.AccountId).HasColumnName("accountID");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.InstructorId)
                    .HasMaxLength(400)
                    .HasColumnName("instructorID");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.SocialMediaAccounts)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_SocialMediaAccount_Instructor");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(400)
                    .HasColumnName("studentID");

                entity.Property(e => e.ContributionNum).HasColumnName("contributionNum");

                entity.HasOne(d => d.StudentNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_User");

                entity.HasMany(d => d.Subs)
                    .WithMany(p => p.Students)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserInterest",
                        l => l.HasOne<SubCategory>().WithMany().HasForeignKey("SubId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserInterests_SubCategory"),
                        r => r.HasOne<Student>().WithMany().HasForeignKey("StudentId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserInterests_Student"),
                        j =>
                        {
                            j.HasKey("StudentId", "SubId");

                            j.ToTable("UserInterests");

                            j.IndexerProperty<string>("StudentId").HasMaxLength(400).HasColumnName("studentID");

                            j.IndexerProperty<int>("SubId").HasColumnName("subID");
                        });
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(e => e.SubId);

                entity.ToTable("SubCategory");

                entity.Property(e => e.SubId).HasColumnName("subID");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.SubName)
                    .HasMaxLength(30)
                    .HasColumnName("subName");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategory_Category");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .HasMaxLength(400)
                    .HasColumnName("userID");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("firstName"); 

                entity.Property(e => e.LastName).HasColumnName("lastName");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Pic)
                    .HasMaxLength(50)
                    .HasColumnName("pic");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("date")
                    .HasColumnName("registerDate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserType).HasColumnName("userType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}