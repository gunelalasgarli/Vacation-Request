using Domain;
using Domain.Entities.Common;
using Domain.Entities.HomeModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace VacationUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager, ILogger<HomeController> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<IActionResult> RequestsView()
        {
            
            List<VacationRequest> requests = await _context.VacationRequests.Include(x => x.VacationType).OrderByDescending(x => x.CreatedAt).Where(x=>x.AppUser.UserName==User.Identity.Name).ToListAsync();
            ViewBag.VacationTypes = await _context.VacationTypes.ToListAsync();

            return View(requests);
        }
        public async Task<IActionResult> Index()
        {
            VacationRequest request = await _context.VacationRequests.Include(p => p.VacationType).FirstOrDefaultAsync();
            ViewBag.VacationTypes = await _context.VacationTypes.ToListAsync();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(VacationRequest request)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");

            }
            else
            {
                
                ViewBag.VacationTypes = await _context.VacationTypes.ToListAsync();

                TempData["Success"] = false;

                if (!await _context.VacationTypes.AnyAsync(x => x.Id == request.VacationTypeId))
                {
                    ModelState.AddModelError("VacationType", "Xetaniz var!");
                }
                if (!ModelState.IsValid)
                {
                    return View();
                }
                int count = 1;
                List<VacationRequest> requests = await _context.VacationRequests.Where(x => x.AppUser.UserName == User.Identity.Name).ToListAsync();
                foreach (var item in requests)
                {
                    count++;
                }
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                request.StatusNum = (int)Domain.Entities.HomeModel.Enums.Status.New;
                request.AppUserId = appUser.Id;
                request.CreatedAt = DateTime.Now;
                request.RequestNo = "Q-" + DateTime.Now.ToString("yyyy-MM-dd") + "-" + count.ToString("D4");

                await _context.VacationRequests.AddAsync(request);
                await _context.SaveChangesAsync();
                TempData["Success"] = true;

               


            }
            return RedirectToAction("index");

        }
    }
}
