using Domain;
using Domain.Entities.Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VacationUI.ViewModels.AccountViewModel;

namespace VacationUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;

        public AccountController(
                                AppDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email or Password is incorrect!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is incorrect!");
                return View();
            }
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM signup)
        {
            if (!ModelState.IsValid) return View();
            AppUser newUser = new AppUser()
            {
                FullName = signup.FullName,
                UserName=signup.UserName,
                Email = signup.Email
            };
            bool isExistUserName = _userManager.Users.Any(us => us.UserName == newUser.UserName);
            if (isExistUserName)
            {
                ModelState.AddModelError("", "This username already exist. Please use another username");
                return View();
            }

            bool isExistEmail = _userManager.Users.Any(us => us.Email == newUser.Email);
            if (isExistEmail)
            {
                ModelState.AddModelError("", "This Email already exist. Please use another email");
                return View();
            }

            IdentityResult identityResult = await _userManager.CreateAsync(newUser, signup.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }

            await _userManager.AddToRoleAsync(newUser, "User");
            await _signInManager.SignInAsync(newUser, true);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            TempData["Success"] = false;


            if (string.IsNullOrWhiteSpace(email))
            {
                return RedirectToAction("Index", "Error");
            }
            AppUser appUser = await _userManager.FindByEmailAsync(email);

            if (appUser == null)
                return RedirectToAction("Index", "Error");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Reset Password", "blueoceanelexample@gmail.com"));

            message.To.Add(new MailboxAddress(appUser.FullName, appUser.Email));
            message.Subject = "Reset Password";

            string emailbody = string.Empty;

            using (StreamReader streamReader = new StreamReader(Path.Combine(_env.WebRootPath, "templates", "forgotpassword.html")))
            {
                emailbody = streamReader.ReadToEnd();
            }

            string forgotpasswordtoken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            string url = Url.Action("changepassword", "account", new { Id = appUser.Id, token = forgotpasswordtoken }, Request.Scheme);

            emailbody = emailbody.Replace("{{fullname}}", $"{appUser.FullName}").Replace("{{url}}", $"{url}");

            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("blueoceanelexample@gmail.com", "Blue@123");
            smtp.Send(message);
            smtp.Disconnect(true);

            TempData["Success"] = true;

            return RedirectToAction("ForgotPassword");
        }

        public async Task<IActionResult> ChangePassword(string Id, string token)
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(token))
            {
                return RedirectToAction("Index", "Error");
            }

            AppUser appuser = await _userManager.FindByIdAsync(Id);
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(appuser);
            if (appuser == null)
            {
                return RedirectToAction("Index", "Error");
            }

            ResetPasswordVM resetPasswordVM = new ResetPasswordVM
            {
                Id = Id,
                Token = resetToken
            };

            return View(resetPasswordVM);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (string.IsNullOrWhiteSpace(resetPasswordVM.Id) || string.IsNullOrWhiteSpace(resetPasswordVM.Token))
            {
                return RedirectToAction("Index", "Error");
            }

            AppUser appuser = await _userManager.FindByIdAsync(resetPasswordVM.Id);
            if (appuser == null)
            {
                return RedirectToAction("Index", "Error");
            }

            IdentityResult identityResult = await _userManager.ResetPasswordAsync(appuser, resetPasswordVM.Token, resetPasswordVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(resetPasswordVM);
            }
            return RedirectToAction("Login");
        }

        #region Add Role
        //public async Task<IActionResult> AddRole()
        //{
        //    if (!await _roleManager.RoleExistsAsync("Admin"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    }
        //    if (!await _roleManager.RoleExistsAsync("User"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    }

        //    return Content("Role Yarandi");
        //}
        #endregion
    }
}
