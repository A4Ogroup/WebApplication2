using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication2.Models;
using WebApplication2.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<LconsultDBContext>(

    options => options.UseSqlServer(builder.Configuration.GetConnectionString("LconsultDBConnection")));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositroy>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddSingleton<ISearchResultService, SearchResultService>();
builder.Services.AddIdentity<User, IdentityRole>(
               options => options.Password.RequireDigit = true
               ).
               AddEntityFrameworkStores<LconsultDBContext>();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Sets the time the session can be idle before it's abandoned.
    options.Cookie.HttpOnly = true; // Prevents the cookie from being accessed via JavaScript.
    options.Cookie.IsEssential = true; // Marks the session cookie as essential for making the application function correctly.
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Homee}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
