using Job_Search_App.Models;
using Job_Search_App.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Job_Search_App.Controllers
{
    [Route("accounts/")]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signManager = signManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("employer/register")]
        public IActionResult EmployerRegister()
        {
            return View();
        }

        [HttpPost]
        [Route("employer/register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployerRegister(
            [Bind("FirstName", "LastName", "Email", "Password", "ConfirmPassword")]
            EmployerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.FirstName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    bool checkRole = await _roleManager.RoleExistsAsync("Employer");
                    if (!checkRole)
                    {
                        var role = new IdentityRole();
                        role.Name = "Employer";
                        await _roleManager.CreateAsync(role);

                        await _userManager.AddToRoleAsync(user, "Employer");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Employer");
                    }

                    TempData["type"] = "success";
                    TempData["message"] = "You Have Registered As a Employer successfully";
                    //await _signManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

           


            return View();
        }

        [HttpGet]
        [Route("employee/register")]
        public IActionResult EmployeeRegister()
        {
            return View();
        }

        [HttpPost]
        [Route("employee/register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeRegister(
            [Bind("FirstName", "LastName", "Email", "Password", "ConfirmPassword" ,"Streetaddress" , "City", "Country" , "Telephonenumber")]
            EmployeeRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.FirstName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Birthday = model.Birthday,
                    Streetaddress = model.Streetaddress,
                    Zipcode = model.Zipcode,
                    City = model.City,
                    Country= model.Country,
                    Telephonenumber = model.Telephonenumber,

                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    bool checkRole = await _roleManager.RoleExistsAsync("Employee");
                    if (!checkRole)
                    {
                        var role = new IdentityRole();
                        role.Name = "Employee";
                        await _roleManager.CreateAsync(role);

                        await _userManager.AddToRoleAsync(user, "Employee");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Employee");
                    }

                    TempData["type"] = "success";
                    TempData["message"] = "You Have Registered As a Job-seeker successfully";

                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel {ReturnUrl = returnUrl};
            return View(model);
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                var userName = user.UserName;

                var result = await _signManager.PasswordSignInAsync(userName,
                    model.Password, model.RememberMe, lockoutOnFailure: false);


                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        [Route("employee/edit-profile")]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return View(user);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        [Route("employee/update-profile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile([FromForm] User model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Gender = model.Gender;

            user.Birthday = model.Birthday;
            user.Streetaddress = model.Streetaddress;
            user.Zipcode = model.Zipcode;
            user.City = model.City;
            user.Country = model.Country;
            user.Telephonenumber = model.Telephonenumber;
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return RedirectToActionPermanent("updateSuccess", "Account");
        }

        [HttpGet]
        public IActionResult updateSuccess()
        {
            return View();
        }
    }
}
