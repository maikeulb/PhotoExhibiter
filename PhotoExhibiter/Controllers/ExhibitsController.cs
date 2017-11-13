using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Data;
using PhotoExhibiter.Models;
using PhotoExhibiter.ViewModels;

namespace PhotoExhibiter.Controllers
{
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
        public IActionResult Mine ()
        {
            var userId = _userManager.GetUserId (User);
            var exhibits = _context.Exhibits
                .Where (e => e.PhotographerId == userId &&
                    e.DateTime > DateTime.Now)
                .Include (e => e.Genre)
                .ToList ();

            return View (exhibits);
        }

        [Authorize]
        public IActionResult Attending ()
        {
            var userId = _userManager.GetUserId (User);
            var exhibits = _context.Attendances
                .Where (a => a.AttendeeId == userId)
                .Select (a => a.Exhibit)
                .Include (e => e.Photographer)
                .Include (e => e.Genre)
                .ToList ();

            var viewModel = new ExhibitsViewModel ()
            {
                UpcomingExhibits = exhibits,
                ShowActions = _signInManager.IsSignedIn (User),
                Heading = "Exhibits I'm Attending"
            };

            return View ("Exhibits", viewModel);
        }

        [Authorize]
        public IActionResult Create ()
        {
            var viewModel = new ExhibitFormViewModel
            {
                Genres = _context.Genres.ToList (),
                Heading = "Add an Exhibit"
            };

            return View ("ExhibitForm", viewModel);
        }

        [Authorize]
        public IActionResult Edit (int id)
        {
            var userId = _userManager.GetUserId (User);
            var exhibit = _context.Exhibits.Single (e => e.Id == id &&
                e.PhotographerId == userId);

            var viewModel = new ExhibitFormViewModel
            {
                Heading = "Edit an Exhibit",
                Id = exhibit.Id,
                Genres = _context.Genres.ToList (),
                Date = exhibit.DateTime.ToString ("d MMM yyyy"),
                Time = exhibit.DateTime.ToString ("HH:mm"),
                Genre = exhibit.GenreId,
                Location = exhibit.Location
            };

            return View ("ExhibitForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (ExhibitFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList ();
                return View ("ExhibitForm", viewModel);
            }

            var exhibit = new Exhibit
            {
                PhotographerId = _userManager.GetUserId (User),
                DateTime = viewModel.GetDateTime (),
                GenreId = viewModel.Genre,
                Location = viewModel.Location
            };

            _context.Exhibits.Add (exhibit);
            _context.SaveChanges ();

            return RedirectToAction ("Mine", "Exhibits");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update (ExhibitFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList ();
                return View ("ExhibitForm", viewModel);
            }

            var userId = _userManager.GetUserId (User);
            var exhibit = _context.Exhibits.Single (e => e.Id == viewModel.Id &&
                e.PhotographerId == userId);
            exhibit.Location = viewModel.Location;
            exhibit.DateTime = viewModel.GetDateTime ();
            exhibit.GenreId = viewModel.Genre;

            _context.SaveChanges ();

            return RedirectToAction ("Mine", "Exhibits");
        }
    }
}
