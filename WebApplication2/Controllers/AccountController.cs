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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                User userModel = await _userManager.FindByEmailAsync(login.Email);

                if (userModel != null)
                {

                    bool found = await _userManager.CheckPasswordAsync(userModel, login.Password);
                    if (found)
                    {
                        await _signInManager.SignInAsync(userModel, login.RememberMe);
                        return RedirectToAction("Index", "instructor");
                    }
                }
                else { ModelState.AddModelError("", "Email and Password invalid"); }

            }
            return View(login);
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

                IdentityResult result = await _userManager.CreateAsync(userModel, _instructorModel.Password);

                if (result.Succeeded == true)
                {
                    await _userManager.AddToRoleAsync(userModel, "instructor");
                    await _signInManager.SignInAsync(userModel, isPersistent: false);
                    TempData["Success"] = "Account created successfully!";
                    return RedirectToAction("login", "account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View("forinstructor");
        }

        
        
        [HttpGet]
        public IActionResult AdminRegister()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminRegister(AdminRegisterViewModel _adminModel)
        {
            if (ModelState.IsValid)
            {

                User userModel = new User();
                userModel.UserName = _adminModel.UserName;
                userModel.FirstName = _adminModel.FirstName;
                userModel.LastName = _adminModel.LastName;
                userModel.Email = _adminModel.Email;
                userModel.PasswordHash = _adminModel.Password;

                IdentityResult result = await _userManager.CreateAsync(userModel, _adminModel.Password);

                if (result.Succeeded == true)
                {
                    await _userManager.AddToRoleAsync(userModel, "Admin");
                    await _signInManager.SignInAsync(userModel, isPersistent: false);
                    TempData["Success"] = "Account created successfully!";
                    return RedirectToAction("login", "account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View("AdminRegister");
        }
        
        
        [HttpGet]
        public IActionResult StudentRegister()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> StudentRegister(StudentRegisterViewModel _studentModel)
        {
            if (ModelState.IsValid)
            {

                User userModel = new User();
                userModel.UserName = _studentModel.UserName;
                userModel.FirstName = _studentModel.FirstName;
                userModel.LastName = _studentModel.LastName;
                userModel.Email = _studentModel.Email;
                userModel.PasswordHash = _studentModel.Password;

                IdentityResult result = await _userManager.CreateAsync(userModel, _studentModel.Password);

                if (result.Succeeded == true)
                {
                    await _userManager.AddToRoleAsync(userModel, "Student");
                    await _signInManager.SignInAsync(userModel, isPersistent: false);
                    TempData["Success"] = "Account created successfully!";
                    return RedirectToAction("login", "account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View("StudentRegister");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");

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

        public async Task<IActionResult> IsEmailAlreadyRegistered(string Email)
        {

            User _user = await _userManager.FindByEmailAsync(Email);

            if (_user == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
    }
}

