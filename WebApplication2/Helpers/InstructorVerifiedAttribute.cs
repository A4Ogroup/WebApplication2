using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using System.Linq;
using WebApplication2.Models.Repository;
using System.Security.Claims;
namespace WebApplication2.Helpers
{
    

    public class InstructorVerifiedAttribute : TypeFilterAttribute
    {
        public InstructorVerifiedAttribute() : base(typeof(InstructorVerifiedFilter))
        {
        }

        private class InstructorVerifiedFilter : IAuthorizationFilter
        {
            private readonly IInstructorRepository _instructorRepository;
            private readonly UserManager<User> _userManager;
            private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

            public InstructorVerifiedFilter(IInstructorRepository instructorRepository, UserManager<User> userManager, ITempDataDictionaryFactory tempDataDictionaryFactory)
            {
                _instructorRepository = instructorRepository;
               _userManager= userManager;
                _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var user = context.HttpContext.User;
                var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                if (userIdClaim == null || roleClaim == null || roleClaim.Value != "Instructor" || !_instructorRepository.IsInstructorVerified(userIdClaim.Value))
                {
                    var tempData = _tempDataDictionaryFactory.GetTempData(context.HttpContext);

                    var alertMessage = " You can not add your Courses at the moment, untill your information is verified !!";
                    var routeValues = context.ActionDescriptor.RouteValues;
                    context.Result = new RedirectToActionResult("index", "instructor", new { message = alertMessage }); 
                }
            }
        }
    }

}
