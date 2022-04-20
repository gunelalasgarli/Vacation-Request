using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacationUI.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Admin")]
        [Area("Admin")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
