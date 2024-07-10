using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneNumbers;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public List<SelectList> CountryCodes { get; set; }

        [HttpGet]
        public IActionResult Register()
        {

            SelectList selectList = new SelectList(GetCountryCodes(), "Value", "Text");
           
        ViewBag.CountryCodes = selectList;
            return View("ForInstructor");
        }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Register(InstructorRegisterViewModel _instructorModel)
        {
            if (ModelState.IsValid)
            {

                User userModel = new User();
                userModel.UserName = _instructorModel.UserName;
                userModel.FirstName = _instructorModel.FirstName;
                userModel.LastName = _instructorModel.LastName;
                userModel.Email = _instructorModel.Email;
                userModel.PasswordHash = _instructorModel.Password;

                IdentityResult result= await  _userManager.CreateAsync(userModel);

                if(result.Succeeded==true) {
                await _signInManager.SignInAsync(userModel, isPersistent: false);
                    return RedirectToAction("index", "student");
                }
                else {
                foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View("forinstructor");
        }




        private List<SelectListItem> GetCountryCodes()
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var supportedRegions = phoneNumberUtil.GetSupportedRegions();

            return supportedRegions.Select(region => new SelectListItem
            {
                Value = phoneNumberUtil.GetCountryCodeForRegion(region).ToString(),
                Text = region,
            }).ToList();
        }
    }
}

