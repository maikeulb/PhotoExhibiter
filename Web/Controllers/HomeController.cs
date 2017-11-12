using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Data;
using PhotoExhibiter.Models;
using PhotoExhibiter.ViewModels;

namespace PhotoExhibiter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ApplicationDbContext context,
                SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var upcomingExhibits = _context.Exhibits
                .Include(g => g.Photographer)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now);

            var viewModel = new ExhibitsViewModel
            {
                UpcomingExhibits = upcomingExhibits,
                ShowActions = _signInManager.IsSignedIn(User),
                Heading = "Upcoming Shows"
            };

            return View("Exhibits",viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
