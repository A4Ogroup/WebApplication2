using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication2.Models;
using WebApplication2.ViewModels.InstructorViewModels;
using WebApplication2.ViewModels;
using WebApplication2.ViewModels.StudentViewModels;
using WebApplication2.Models.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;

namespace WebApplication2.Controllers
{
    public class StudentAccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly LconsultDBContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStudentRepository _studentRepository;
        public StudentAccountController(UserManager<User> userManager, SignInManager<User> signInManager, LconsultDBContext context,ICategoryRepository categoryRepository,IStudentRepository studentRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _categoryRepository = categoryRepository;
            _studentRepository = studentRepository;
        }


     

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Category = new SelectList(_categoryRepository.GetAll().Select(C => new
            {
                C.CategoryId,
                C.CategoryName
            }).ToList());

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
                else { ModelState.AddModelError("", "Email or Password is invalid"); }

            }
            return View(login);
        }




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
            var model = new StudentCredentialsViewModel();
            if (TempData.ContainsKey("UserCredentials"))
            {
                model = JsonConvert.DeserializeObject<StudentCredentialsViewModel>(TempData["UserCredentials"].ToString());
            }
            return View("~/Views/Account/StudentRegister/Step1.cshtml", model);
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
                    return View("~/Views/Account/StudentRegister/Step1.cshtml", model);
                }
                TempData["UserCredentials"] = JsonConvert.SerializeObject(model);
                TempData.Keep("UserCredentials");
                return RedirectToAction("Step2");
            }
            return View("~/Views/Account/StudentRegister/Step1.cshtml", model);
        }
        [HttpGet]
        public IActionResult Step2()
        {
            
            var model = new StudentInfoViewModel();
            if (!TempData.ContainsKey("UserCredentials"))
            {
                return RedirectToAction("Step1");
            }
            if (TempData.ContainsKey("UserInfo"))
            {
                model = JsonConvert.DeserializeObject<StudentInfoViewModel>(TempData["UserInfo"].ToString());
            }
            return View("~/Views/Account/StudentRegister/Step2.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Step2(StudentInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["UserInfo"] = JsonConvert.SerializeObject(model);
                TempData.Keep("UserInfo");
                return RedirectToAction("Step3", new {CategoryId=model.CategoryId});
            }
            return View("~/Views/Account/StudentRegister/Step2.cshtml", model);
        }

        public IActionResult Step3(int CategoryId)
        {
            var model = new UserInterestsViewModel();

            var subcategories = _categoryRepository.GetSubCategories(CategoryId).ToList();
            if (subcategories == null || subcategories.Count == 0)
            {
                ModelState.AddModelError("CategoryId", "No subcategories found for the selected category.");
                // Optionally, you can redirect back to Step2 or show a message
                TempData["Error"] = "No subcategories found for the selected category.";
                return RedirectToAction("Step2");
            }

            ViewBag.selectedSubs=_categoryRepository.GetSubCategories(CategoryId)/*.Select(S=> new {S.SubId,S.SubName})*/.ToList();

            if (!TempData.ContainsKey("UserCredentials"))
            {
                return RedirectToAction("Step1");
            }
            if (!TempData.ContainsKey("UserInfo"))
            {
                return RedirectToAction("Step2");
            }

            if (TempData.ContainsKey("UserInterests"))
            {
                model = JsonConvert.DeserializeObject<UserInterestsViewModel>(TempData["UserInterests"].ToString());
            }
            return View("~/Views/Account/StudentRegister/Step3.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Step3(UserInterestsViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["UserInterests"] = JsonConvert.SerializeObject(model);
                TempData.Keep("UserInterests");

                // Combine all data and save to database or perform other actions
                var userCredentials = JsonConvert.DeserializeObject<StudentCredentialsViewModel>(TempData["UserCredentials"].ToString());
                var userInfo = JsonConvert.DeserializeObject<StudentInfoViewModel>(TempData["UserInfo"].ToString());
                var userInterests = JsonConvert.DeserializeObject<UserInterestsViewModel>(TempData["UserInterests"].ToString()); 
                StudentRegisterViewModel studentRegisterViewModel = new()
                {
                    UserName = userCredentials.UserName,
                    Email = userCredentials.Email,
                    Password = userCredentials.Password,
                    ConfirmPassword = userCredentials.ConfirmPassword,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Gender = userInfo.Gender,
                    Picture = userInfo.Picture,
                    SelectedTagIds = userInterests.SelectedTagIds,




                };
                // Save the user data to the database
                // Example: _userService.SaveUser(userCredentials, userInfo, userProfession);
                //  TempData.Remove("UserProfession");
                //TempData.Clear();
               return await Register(studentRegisterViewModel);
            }
            //  return View(model);
            //ViewBag.selectedSubs = _categoryRepository.GetSubCategories(CategoryId)
            return View("~/Views/Account/StudentRegister/Step3.cshtml", model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(StudentRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        User userModel = new User
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            Gender = model.Gender
                        };

                        IdentityResult result = await _userManager.CreateAsync(userModel, model.Password);

                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(userModel, "Student");

                            var student = new Student
                            {
                                StudentId = userModel.Id,
                                StudentNavigation = userModel,
                                UserInterests = model.SelectedTagIds.Select(subId => new UserInterests { SubId = subId }).ToList()
                            };

                            _studentRepository.Add(student);

                            if (await _studentRepository.SaveAsync())
                            {
                                await _signInManager.SignInAsync(userModel, isPersistent: false);

                                await transaction.CommitAsync();
                                TempData["Success"] = "Account created successfully!";
                                TempData.Remove("StudentRegisterViewModel");
                                return RedirectToAction("Login", "Account");
                            }
                            else
                            {
                                // If student save fails, rollback and remove the user
                                await _userManager.DeleteAsync(userModel);
                                await transaction.RollbackAsync();

                                TempData["Errors"] = JsonConvert.SerializeObject(new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("", "Failed to save student data.")
                        });
                            }
                        }
                        else
                        {
                            await transaction.RollbackAsync();

                            List<KeyValuePair<string, string>> errors = result.Errors.Select(e => new KeyValuePair<string, string>("", e.Description)).ToList();
                            TempData["Errors"] = JsonConvert.SerializeObject(errors);
                        }
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }

            return RedirectToAction("Step1");
        }



    }
}
