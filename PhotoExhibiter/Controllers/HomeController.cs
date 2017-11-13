using System;
using System.Diagnostics;
using System.Linq;
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

        public HomeController (ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Index ()
        {
            var upcomingExhibits = _context.Exhibits
                .Include (e => e.Photographer)
                .Include (e => e.Genre)
                .Where (e => e.DateTime > DateTime.Now);

            var viewModel = new ExhibitsViewModel
            {
                UpcomingExhibits = upcomingExhibits,
                ShowActions = _signInManager.IsSignedIn (User),
                Heading = "Upcoming Shows"
            };

            return View ("Exhibits", viewModel);
        }

        public IActionResult About ()
        {
            ViewData["Message"] = "Your application description page.";

            return View ();
        }

        public IActionResult Contact ()
        {
            ViewData["Message"] = "Your contact page.";

            return View ();
        }

        public IActionResult Error ()
        {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}