using Domain;
using Domain.Entities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationUI.ViewModels.AccountViewModel;

namespace VacationUI.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(
            UserManager<AppUser> userManager,
            IWebHostEnvironment env,
            AppDbContext context,
            SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _env = env;
            _context = context;
            _signInManager = signInManager;
        }
        #region Index

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = _userManager.Users
                .OrderByDescending(us => us.FullName).ToList();
            List<AppUserVM> appuserVM = new List<AppUserVM>();
            foreach (AppUser user in users)
            {
                AppUserVM userVM = new AppUserVM
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = (await _userManager.GetRolesAsync(user))[0],
                };
                appuserVM.Add(userVM);
            }
            return View(appuserVM);
        }
        #endregion
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ChangePassword(string id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> ChangePassword(string Id, string Password)
        {
            if (Id == null) return RedirectToAction("Index", "Error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "Error");

            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

            await _userManager.ResetPasswordAsync(appUser, token, Password);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(string Id)
        {
            if (Id == null) return RedirectToAction("Index", "Error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "Error");

            var userRoles = await _userManager.GetRolesAsync(appUser);

            AppUserVM appUserVM = new AppUserVM
            {
                Id = appUser.Id,
                FullName = appUser.FullName,
                Email = appUser.Email,
                UserName = appUser.UserName,
                Roles = new List<string> { "Admin","User" },
                Role = userRoles.Count > 0 ? (await _userManager.GetRolesAsync(appUser))[0] : ""
            };

            return View(appUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]


        public async Task<IActionResult> ChangeRole(string Id, string Roles)
        {
            if (Id == null) return RedirectToAction("Index", "Error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "Error");

            var userRoles = await _userManager.GetRolesAsync(appUser);

            if (userRoles.Count > 0)
            {
                string oldRole = (await _userManager.GetRolesAsync(appUser))[0];

                await _userManager.RemoveFromRoleAsync(appUser, oldRole);
            }


            await _userManager.AddToRoleAsync(appUser, Roles);

            return RedirectToAction("Index");
        }




        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Detail(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

    }
}
