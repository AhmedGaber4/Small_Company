using Data;
using Data.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.Helper;
using PL.Models;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, ILogger<AccountController> logger,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        public IActionResult SignUp()
        {
            return View(new SignUpViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email.Split("@")[0],
                    Isactive = true
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction("Login");

                foreach (var i in result.Errors)
                {
                    _logger.LogError(i.Description);
                    ModelState.AddModelError("", i.Description);
                }

            }
            return View(model);
        }


        public IActionResult Login()
        {
            return View(new SignInViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                    ModelState.AddModelError(" ", "Email Does not Exist");
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        public IActionResult ForgetPassword()
        {

            return View(new ForgetPasswordViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forget)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forget.Email);
                if (user is null)
                    ModelState.AddModelError(" ", "Email Does not Exist");
                if (user != null)
                {
                    var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = forget.Email, token = Token }, Request.Scheme);
                    var email = new Email
                    {
                        Title = "Reset Password",
                        Body = resetPasswordLink,
                        To = forget.Email

                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction("CompleteForgetPassword");
                }

            }
            return View(forget);

        }
        public IActionResult ResetPassword(string email, string token)
        {

            return View(new ResetPasswordViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel Reset)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Reset.Email);
                if (user is null)
                    ModelState.AddModelError(" ", "Email Does not Exist");
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, Reset.Token, Reset.Password);
                    if (result.Succeeded)
                        return RedirectToAction("Login");

                    foreach (var i in result.Errors)
                    {
                        _logger.LogError(i.Description);
                        ModelState.AddModelError("", i.Description);
                    }

                }

            }
                return View(Reset);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
 
}

