using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Account;

namespace PhotoExhibiter.Features.Exhibits
{
    [Authorize]
    public class ExhibitsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ExhibitsController (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mediator = mediator;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index (Index.Query query)
        {
            query.UserId = _userManager.GetUserId (User);
            query.ShowActions = _signInManager.IsSignedIn (User);
            var model = await _mediator.Send (query);

            return View (model);
        }

        [HttpPost]
        public IActionResult Search (Index.Query model)
        {
            return RedirectToAction ("Index", "Exhibits", model);
        }

        public async Task<IActionResult> Create (Create.Query query)
        {
            var model = await _mediator.Send (query);

            return View (model);
        }

        public async Task<IActionResult> Edit (Edit.Query query)
        {
            query.UserId = _userManager.GetUserId (User);

            var modelOrError = await _mediator.Send (query);

            return modelOrError.IsSuccess ?
                (IActionResult) View (modelOrError.Value) :
                (IActionResult) BadRequest (modelOrError.Error);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Create.Command command)
        {
            if (!ModelState.IsValid)
                return View ("Create", command);

            command.UserId = _userManager.GetUserId (User);

            await _mediator.Send (command);

            return RedirectToAction ("Index", "Users");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (Edit.Command command)
        {
            command.UserId = _userManager.GetUserId (User);

            var result = await _mediator.Send (command);

            return result.IsSuccess ?
                (IActionResult) RedirectToAction ("Index", "Users") :
                (IActionResult) BadRequest (result.Error);
        }
    }
}