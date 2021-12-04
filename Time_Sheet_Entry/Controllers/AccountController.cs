using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Time_Sheet_Entry.Data.ViewModels;
using Time_Sheet_Entry.Models;

namespace Time_Sheet_Entry.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                //create roles
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Employee"));
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            List<SelectListItem> listItems = new List<SelectListItem>();

            listItems.Add(new SelectListItem { Value = "Admin", Text = "Admin" });

            listItems.Add(new SelectListItem { Value = "Employee", Text = "Employee" });

            listItems.Add(new SelectListItem { Value = "Manager", Text = "Manager" });


            ViewData["ReturnUrl"] = returnUrl;
            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                RoleList = listItems,
            };

            return View(registerViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    FullName = registerViewModel.FullName,
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    if (registerViewModel != null && registerViewModel.RoleSelected.Length > 0 && registerViewModel.RoleSelected == "Admin")
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    if (registerViewModel != null && registerViewModel.RoleSelected.Length > 0 && registerViewModel.RoleSelected == "Manager")
                    {
                        await _userManager.AddToRoleAsync(user, "Manager");
                    }
                    if (registerViewModel != null && registerViewModel.RoleSelected.Length > 0 && registerViewModel.RoleSelected == "Employee")
                    {
                        await _userManager.AddToRoleAsync(user, "Employee");
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                AddErrors(result);
            }

            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [HttpGet]
        public IActionResult LogIn(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInViewModel logInViewModel, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(logInViewModel.Email, logInViewModel.Password, isPersistent: logInViewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
                    return View(logInViewModel);
                }
            }

            return View(logInViewModel);
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
