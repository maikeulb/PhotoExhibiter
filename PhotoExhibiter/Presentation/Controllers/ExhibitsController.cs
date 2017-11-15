using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Presentation.ViewModels;

namespace PhotoExhibiter.Presentation.Controllers
{
    public class ExhibitsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public ExhibitsController (UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public IActionResult Mine ()
        {
            var userId = _userManager.GetUserId (User);
            var exhibits = _unitOfWork.Exhibits.GetUpcomingExhibitsByPhotographer (userId);

            return View (exhibits);
        }

        [Authorize]
        public IActionResult Attending ()
        {
            var userId = _userManager.GetUserId (User);

            var model = new ExhibitsViewModel ()
            {
                UpcomingExhibits = _unitOfWork.Exhibits.GetExhibitsUserAttending(userId),
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
                Genres = _unitOfWork.Genres.GetGenres (),
                Heading = "Add an Exhibit"
            };

            return View ("ExhibitForm", model);
        }

        [Authorize]
        public IActionResult Edit (int id)
        {
            var exhibit = _unitOfWork.Exhibits.GetExhibit (id);

            if (exhibit == null)
                return NotFound ();

            if (exhibit.PhotographerId != _userManager.GetUserId (User))
                return new UnauthorizedResult ();

            var model = new ExhibitFormViewModel
            {
                Heading = "Edit an Exhibit",
                Id = exhibit.Id,
                Genres = _unitOfWork.Genres.GetGenres (),
                Date = exhibit.DateTime.ToString ("d MMM yyyy"),
                Time = exhibit.DateTime.ToString ("HH:mm"),
                Genre = exhibit.GenreId,
                Location = exhibit.Location
            };

            return View ("ExhibitForm", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (ExhibitFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _unitOfWork.Genres.GetGenres ();
                return View ("ExhibitForm", model);
            }

            var exhibit = new Exhibit
            {
                PhotographerId = _userManager.GetUserId (User),
                DateTime = model.GetDateTime (),
                GenreId = model.Genre,
                Location = model.Location
            };

            _unitOfWork.Exhibits.Add (exhibit);
            _unitOfWork.Complete ();

            return RedirectToAction ("Mine", "Exhibits");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update (ExhibitFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _unitOfWork.Genres.GetGenres ();
                return View ("ExhibitForm", model);
            }

            var exhibit = _unitOfWork.Exhibits.GetExhibitWithAttendees (model.Id);

            if (exhibit == null)
                return NotFound ();

            if (exhibit.PhotographerId != _userManager.GetUserId (User))
                return new UnauthorizedResult ();

            exhibit.Modify (model.GetDateTime (), model.Location, model.Genre);

            _unitOfWork.Complete ();

            return RedirectToAction ("Mine", "Exhibits");
        }
    }
}
