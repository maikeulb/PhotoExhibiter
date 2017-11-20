using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Application;
using PhotoExhibiter.Application.Commands;
using PhotoExhibiter.Application.Queries;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.WebUI.Controllers
{
    public class ExhibitsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IExhibitService _exhibitService;

        public ExhibitsController (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IMediator mediator,
            IExhibitService exhibitService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mediator = mediator;
            _exhibitService = exhibitService;
        }

        [Authorize]
        public async Task<IActionResult> Mine (Mine.Query query)
        {
            query.UserId = _userManager.GetUserId (User);

            var model = await _mediator.Send (query);

            return View (model);
        }

        [Authorize]
        public async Task<IActionResult> Attending (Attending.Query query)
        {
            query.UserId = _userManager.GetUserId (User);
            query.ShowActions = _signInManager.IsSignedIn (User);

            var model = await _mediator.Send (query);

            return View (model);
        }

        [Authorize]
        public async Task<IActionResult> Create (Create.Query query)
        {
            var model = await _mediator.Send (query);

            return View (model);
        }

        [Authorize]
        public async Task<IActionResult> Edit (Edit.Query query)
        {
            query.UserId = _userManager.GetUserId (User);

            var exhibit = _exhibitService.GetExhibit (query.Id);
            if (exhibit == null)
                return NotFound ();

            var isPhotographer = _exhibitService.IsPhotographerExhibitOwner (exhibit, query.UserId);
            if (isPhotographer == false)
                return Unauthorized ();

            var model = await _mediator.Send (query);
            return View (model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Create.Command command)
        {
            command.UserId = _userManager.GetUserId (User);

            if (!ModelState.IsValid)
                return View ("Create", command);

            await _mediator.Send (command);

            return RedirectToAction ("Mine", "Exhibits");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (Edit.Command command)
        {
            command.UserId = _userManager.GetUserId (User);

            var exhibit = _exhibitService.GetExhibit (command.Id);
            if (exhibit == null)
                return NotFound ();

            var isPhotographer = _exhibitService.IsPhotographerExhibitOwner (exhibit, command.UserId);
            if (isPhotographer == false)
                return Unauthorized ();

            await _mediator.Send (command);

            return RedirectToAction ("Mine", "Exhibits");
        }
    }
}