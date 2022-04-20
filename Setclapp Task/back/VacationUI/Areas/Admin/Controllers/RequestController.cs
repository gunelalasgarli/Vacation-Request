using Domain;
using Domain.Entities.Common;
using Domain.Entities.HomeModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacationUI.Areas.Admin.Controllers
{
    public class RequestController : Controller
    {
        //[Authorize(Roles = "Admin")]
        //[Area("Admin")]

        //private readonly AppDbContext _context;
        //private readonly UserManager<AppUser> _userManager;

        //public RequestController(AppDbContext context, UserManager<AppUser> userManager)
        //{
        //    _context = context;
        //    _userManager = userManager;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> SendRequest(VacationRequest request)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Login", "Account");

        //    }
        //    else
        //    {
        //        TempData["Success"] = false;

        //        if (!ModelState.IsValid) return View();

        //        AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

        //        request.AppUserId = appUser.Id;
        //        request.CreatedAt = DateTime.Now;

        //        //await _context.VacationRequest.AddAsync(request);
        //        await _context.SaveChangesAsync();


        //        TempData["Success"] = true;
        //    }

        //    return RedirectToAction("Index", "Contact");
        }
    }

