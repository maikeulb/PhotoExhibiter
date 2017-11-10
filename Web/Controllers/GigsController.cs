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

        public GigsController () {
            _context = new ApplicationDbContext ();
        }

        [Authorize]
        public IActionResult Create () {

            var viewModel = new GigFormViewModel();

            return View (viewModel);
        }

         [Authorize]
         [HttpPost]
         public IActionResult Create (GigFormViewModel viewModel)

        {
            var gig = new Gig
            {
                PhotographerId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                DateTime = viewModel.DateTime,
                GenreId = viewModel.Genre,
                Location = viewModel.Location
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}
