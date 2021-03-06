using Domain;
using Domain.Entities.Common;
using Domain.Entities.HomeModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacationUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]

    public class RequestController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RequestController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<VacationRequest> requests = await _context.VacationRequests.Include(x=>x.VacationType).OrderByDescending(x => x.CreatedAt).ToListAsync();
            ViewBag.VacationTypes = await _context.VacationTypes.ToListAsync();
            if (requests == null)
            {
                return RedirectToAction("index");
            }

            return View(requests);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Error");
            VacationRequest request = await _context.VacationRequests.Include(x => x.VacationType).FirstOrDefaultAsync(c => c.Id == id);
            ViewBag.VacationTypes = await _context.VacationTypes.ToListAsync();

            return View(request);
        }
        public async Task<IActionResult> Check(int id)
        {
            VacationRequest request = await _context.VacationRequests.FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
            {
                return RedirectToAction("index");
            }
            request.StatusNum = (int)Domain.Entities.HomeModel.Enums.Status.Pending;


            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Accepted(int id)
        {
            VacationRequest request = await _context.VacationRequests.FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
            {
                return RedirectToAction("index");
            }
            request.StatusNum = (int)Domain.Entities.HomeModel.Enums.Status.Accepted;
            request.AcceptedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Rejected(int id)
        {
            VacationRequest request = await _context.VacationRequests.FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
            {
                return RedirectToAction("index");
            }
            request.StatusNum = (int)Domain.Entities.HomeModel.Enums.Status.Rejected;


            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        

    }
    }

