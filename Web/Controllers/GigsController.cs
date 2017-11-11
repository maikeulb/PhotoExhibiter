using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers {
    public class GigsController : Controller {
        private readonly ApplicationDbContext _context;

        public GigsController (ApplicationDbContext context) {
            _context = context;
        }

        [Authorize]
        public IActionResult Create ()
        {

            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList ()
            };

            return View (viewModel);
        }

        [Authorize]
        [HttpPost]
         [ValidateAntiForgeryToken]
        public IActionResult Create (GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }

            var gig = new Gig {
                PhotographerId = User.FindFirstValue (ClaimTypes.NameIdentifier),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Location = viewModel.Location
            };

            _context.Gigs.Add (gig);
            _context.SaveChanges ();

            return RedirectToAction ("Index", "Home");
        }

    }
}
