using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Domain.Commands;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Queries;

namespace PhotoExhibiter.WebUI.Controllers
{
    public class ExhibitsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IExhibitRepository _exhibitrepository;
        private readonly IGenreRepository _genrerepository;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ExhibitsController (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IExhibitRepository exhibitrepository,
            IGenreRepository genrerepository,
            IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _exhibitrepository = exhibitrepository;
            _genrerepository = genrerepository;
            _mediator = mediator;
        }

        [Authorize]
        public async Task<IActionResult> Mine ()
        {
            var query = new GetMyExhibitsQuery
            {
                UserId = _userManager.GetUserId (User),
            };

            var model = await _mediator.Send(query);

            return View (model);
        }

        [Authorize]
        public async Task<IActionResult> Attending ()
        {
            var query = new GetMyAttendingExhibitsQuery
            {
                UserId = _userManager.GetUserId (User),
                ShowActions = _signInManager.IsSignedIn (User),
            };

            var model = await _mediator.Send(query);

            return View ("Exhibits", model);
        }

        [Authorize]
        public async Task<IActionResult> Create (CreateExhibitQuery query)
        {
            var model = await _mediator.Send(query);

            return View ("Create", model);
        }

        [Authorize]
        public async Task<IActionResult> Edit (EditExhibitQuery query)
        {
            // Validation
            var exhibit = _exhibitrepository.GetExhibit (query.Id);
            if (exhibit == null)
                return NotFound ();

            if (exhibit.PhotographerId != _userManager.GetUserId (User))
                return new UnauthorizedResult ();
            // Validation

            query.UserId = _userManager.GetUserId(User);

            var model = await _mediator.Send(query);

            return View ("Edit", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (CreateExhibitCommand command)
        {

            // Validation
            if (!ModelState.IsValid)
            {
                command.Genres = _genrerepository.GetGenres ();
                return View ("Create", command);
            }
            // Validation

            command.UserId =_userManager.GetUserId(User);

            await _mediator.Send(command);

            return RedirectToAction ("Mine", "Exhibits");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (EditExhibitCommand command)
        {
            //validation
            if (!ModelState.IsValid)
            {
                command.Genres = _genrerepository.GetGenres ();
                return View ("Edit", command);
            }

            var exhibit = _exhibitrepository.GetExhibitWithAttendees (command.Id);

            if (exhibit == null)
                return NotFound ();

            if (exhibit.PhotographerId != _userManager.GetUserId (User))
                return new UnauthorizedResult ();
            //validation

            await _mediator.Send(command);

            return RedirectToAction ("Mine", "Exhibits");
        }
    }
}
