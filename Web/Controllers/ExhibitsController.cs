using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Data;
using PhotoExhibiter.Models;
using PhotoExhibiter.ViewModels;

namespace PhotoExhibiter.Controllers {

    public class ExhibitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ExhibitsController (ApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = _userManager.GetUserId(User);
            /* PhotographerId = User.FindFirstValue(ClaimTypes.NameIdentifier) */
            var exhibits = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Exhibit)
                .Include(g => g.Photographer)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new ExhibitsViewModel()
            {
                UpcomingExhibits = exhibits,
                ShowActions = _signInManager.IsSignedIn(User),
                Heading = "Exhibitions I'm Attending"
            };

            return View("Exhibits", viewModel);
        }

        [Authorize]
        public IActionResult Create ()
        {

            var viewModel = new ExhibitFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (ExhibitFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }

            var exhibit = new Exhibit
            {
                PhotographerId= _userManager.GetUserId(User),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Location = viewModel.Location
            };

            _context.Exhibits.Add (exhibit);
            _context.SaveChanges ();

            return RedirectToAction ("Index", "Home");
        }

    }
}
