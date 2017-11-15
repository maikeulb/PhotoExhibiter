using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Presentation.ViewModels;

namespace PhotoExhibiter.Presentation.Controllers
{
    public class ExhibitsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IExhibitRepository _exhibitrepository;
        private readonly IGenreRepository _genrerepository;
        private readonly ILogger _logger;

        public ExhibitsController (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IExhibitRepository exhibitrepository,
            IGenreRepository genrerepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _exhibitrepository = exhibitrepository;
            _genrerepository = genrerepository;
        }

        [Authorize]
        public IActionResult Mine ()
        {
            var userId = _userManager.GetUserId (User);
            var exhibits = _exhibitrepository.GetUpcomingExhibitsByPhotographer (userId);

            return View (exhibits);
        }

        [Authorize]
        public IActionResult Attending ()
        {
            var userId = _userManager.GetUserId (User);

            var model = new ExhibitsViewModel ()
            {
                UpcomingExhibits = _exhibitrepository.GetExhibitsUserAttending(userId),
                ShowActions = _signInManager.IsSignedIn (User),
                Heading = "Exhibits I'm Attending"
            };

            return View ("Exhibits", model);
        }

        [Authorize]
        public IActionResult Create ()
        {
            var model = new ExhibitFormViewModel
            {
                Genres = _genrerepository.GetGenres (),
                Heading = "Add an Exhibit"
            };

            return View ("ExhibitForm", model);
        }

        [Authorize]
        public IActionResult Edit (int id)
        {
            var exhibit = _exhibitrepository.GetExhibit (id);
            _logger.LogInformation ("exhibit", exhibit);

            if (exhibit == null)
                return NotFound ();

            if (exhibit.PhotographerId != _userManager.GetUserId (User))
                return new UnauthorizedResult ();

            var model = new ExhibitFormViewModel
            {
                Id = exhibit.Id,
                Location = exhibit.Location,
                Date = exhibit.DateTime.ToString ("d MMM yyyy"),
                Time = exhibit.DateTime.ToString ("HH:mm"),
                Genre = exhibit.GenreId,
                Genres = _genrerepository.GetGenres (),
                Heading = "Edit an Exhibit"
            };

            _logger.LogInformation ("Genres", model.Genres);

            return View ("ExhibitForm", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (ExhibitFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _genrerepository.GetGenres ();
                return View ("ExhibitForm", model);
            }

            var exhibit = new Exhibit
            {
                PhotographerId = _userManager.GetUserId (User),
                DateTime = model.GetDateTime (),
                GenreId = model.Genre,
                Location = model.Location
            };

            _exhibitrepository.Add (exhibit);
            _exhibitrepository.SaveAll ();

            return RedirectToAction ("Mine", "Exhibits");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update (ExhibitFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _genrerepository.GetGenres ();
                return View ("ExhibitForm", model);
            }

            var exhibit = _exhibitrepository.GetExhibitWithAttendees (model.Id);

            if (exhibit == null)
                return NotFound ();

            if (exhibit.PhotographerId != _userManager.GetUserId (User))
                return new UnauthorizedResult ();

            exhibit.Modify (model.GetDateTime (), model.Location, model.Genre);
            _exhibitrepository.SaveAll ();

            return RedirectToAction ("Mine", "Exhibits");
        }
    }
}
