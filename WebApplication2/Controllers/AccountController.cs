﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PhoneNumbers;
using WebApplication2.Models;
using WebApplication2.ViewModels;
using WebApplication2.ViewModels.InstructorViewModels;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly LconsultDBContext _context;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,LconsultDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context= context;
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

        //[HttpGet]
        //public IActionResult Register()
        //{

        //    SelectList selectList = new SelectList(GetCountryCodes(), "Value", "Text");

        //    ViewBag.CountryCodes = selectList;
        //    return View("ForInstructor");
        //}

        [HttpGet]
        public IActionResult Step1()
        {

            if (TempData["Errors"] != null)
            {
                var errors = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(TempData["Errors"].ToString());
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            var model = new InstructorCredentialsViewModel();
            if (TempData.ContainsKey("UserCredentials"))
            {
                model = JsonConvert.DeserializeObject<InstructorCredentialsViewModel>(TempData["UserCredentials"].ToString());
            }
            return View("~/Views/Account/instructorRegister/Step1.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step1(InstructorCredentialsViewModel model)
        {

            
            if (ModelState.IsValid)
            {
                if (model.Password.Length < 6)
                {
                    ModelState.AddModelError("Password", "Password must be at least 6 characters long.");
                }
                if (!model.Password.Any(char.IsLower))
                {
                    ModelState.AddModelError("Password", "Passwords must have at least one lowercase ('a'-'z').");
                }
                if (!model.Password.Any(char.IsUpper))
                {
                    ModelState.AddModelError("Password", "Passwords must have at least one uppercase ('A'-'Z').");
                }
                if (!model.Password.Any(char.IsDigit))
                {
                    ModelState.AddModelError("Password", "Password must contain at least one number.");
                }

                if (!ModelState.IsValid)
                {
                    return View("~/Views/Account/instructorRegister/Step1.cshtml", model);
                }
                TempData["UserCredentials"] = JsonConvert.SerializeObject(model);
                TempData.Keep("UserCredentials");
                return RedirectToAction("Step2"); 
            }
            return View("~/Views/Account/instructorRegister/Step1.cshtml", model);
        }
        [HttpGet]
        public IActionResult Step2()
        {
            var model = new InstructorInfoViewModel();
            if (!TempData.ContainsKey("UserCredentials"))
            {
                return RedirectToAction("Step1");
            }
            if (TempData.ContainsKey("UserInfo"))
            {
                model = JsonConvert.DeserializeObject<InstructorInfoViewModel>(TempData["UserInfo"].ToString());
            }
            return View("~/Views/Account/instructorRegister/Step2.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step2(InstructorInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["UserInfo"] = JsonConvert.SerializeObject(model);
                TempData.Keep("UserInfo");
                return RedirectToAction("Step3");
            }
            return View("~/Views/Account/instructorRegister/Step2.cshtml", model);
        }

        public IActionResult Step3()
        {
            var model = new InstructorProfessionViewModel();
            if (!TempData.ContainsKey("UserCredentials"))
            {
                return RedirectToAction("Step1");
            }
            if (!TempData.ContainsKey("UserInfo"))
            {
                return RedirectToAction("Step2");
            }

            if (TempData.ContainsKey("UserProfession"))
            {
                model = JsonConvert.DeserializeObject<InstructorProfessionViewModel>(TempData["UserProfession"].ToString());
            }
            return View("~/Views/Account/instructorRegister/Step3.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step3(InstructorProfessionViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["UserProfession"] = JsonConvert.SerializeObject(model);
                TempData.Keep("UserProfession");

                // Combine all data and save to database or perform other actions
                var userCredentials = JsonConvert.DeserializeObject<InstructorCredentialsViewModel>(TempData["UserCredentials"].ToString());
                var userInfo = JsonConvert.DeserializeObject<InstructorInfoViewModel>(TempData["UserInfo"].ToString());
                var userProfession = JsonConvert.DeserializeObject<InstructorProfessionViewModel>(TempData["UserProfession"].ToString());
                InstructorRegisterViewModel instructorRegisterViewModel = new() 
                { 
                    UserName=userCredentials.UserName,
                    Email = userCredentials.Email, 
                    Password =userCredentials.Password,
                    ConfirmPassword=userCredentials.ConfirmPassword, 
                    FirstName=userInfo.FirstName, 
                    LastName=userInfo.LastName,
                    Gender=userInfo.Gender, 
                    PhoneNumber=userInfo.PhoneNumber,
                    Picture=userInfo.Picture,
                    Profession=userProfession.Profession,
                    About=userProfession.About,
                    YearsExperince=userProfession.YearsExperince,
                    Website=userProfession.Website,
                    SocialMediaAccounts=userProfession.SocialMediaAccounts,

                    
                
                
                };
                // Save the user data to the database
                // Example: _userService.SaveUser(userCredentials, userInfo, userProfession);
              //  TempData.Remove("UserProfession");
                //TempData.Clear();
                return await Register(instructorRegisterViewModel);
            }
          //  return View(model);
           return View("~/Views/Account/instructorRegister/Step3.cshtml", model);
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
                userModel.PhoneNumber = _instructorModel.PhoneNumber;
                userModel.Gender= _instructorModel.Gender;
                IdentityResult result = await _userManager.CreateAsync(userModel, _instructorModel.Password);

                if (result.Succeeded == true)
                {
                    await _userManager.AddToRoleAsync(userModel, "Instructor");
                   
                    await _signInManager.SignInAsync(userModel, isPersistent: false);
                    TempData["Success"] = "Account created successfully!";
                    var instructor = new Models.Instructor()
                    {
                        InstructorId = userModel.Id,
                        Profession = _instructorModel.Profession,
                        YearsExperince = _instructorModel.YearsExperince,
                        About = _instructorModel.About,
                        Website = _instructorModel.Website,
                        InstructorNavigation = userModel,
                        SocialMediaAccounts = _instructorModel?.SocialMediaAccounts
                    ?.Where(account => !string.IsNullOrWhiteSpace(account))
                    .Select(account => new SocialMediaAccount
                    {
                        Account = account,
                        InstructorId = userModel.Id // Set the foreign key
                    }).ToList()
                    };

                    _context.Instructors.Add(instructor);
                    _context.SaveChangesAsync();
                    

                    return RedirectToAction("login", "account");
                }
                else
                {
                    List<KeyValuePair<string, string>> erorrs =new List<KeyValuePair<string, string>>();
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        erorrs.Add(new KeyValuePair<string,string>("",item.Description));
                    }
                    //List<KeyValuePair<string, string>> erorrs = ModelState
                    //             .Where(x => x.Value.Errors.Count > 0)
                    //             .Select(x => new KeyValuePair<string, string>(x.Key, x.Value.Errors.First().ErrorMessage))
                    //             .ToList();
                    TempData["Errors"]=JsonConvert.SerializeObject(erorrs);
                }

            }
            return RedirectToAction("Step1");
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

        public async Task<IActionResult> IsUserNameAlreadyExists(string userName)
        {

            User _user = await _userManager.FindByNameAsync(userName);

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
