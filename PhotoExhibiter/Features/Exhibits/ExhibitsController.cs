using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Features;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Features.Account;

namespace PhotoExhibiter.Features.Exhibits
{
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

            var modelOrError = await _mediator.Send (query);

            return modelOrError.IsSuccess
                ? (IActionResult)View(modelOrError.Value)
                : (IActionResult)BadRequest(modelOrError.Error);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Create.Command command)
        {
            if (!ModelState.IsValid)
                return View ("Create", command);

            command.UserId = _userManager.GetUserId (User);

            await _mediator.Send (command);

            return RedirectToAction ("Mine", "Exhibits");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (Edit.Command command)
        {
            command.UserId = _userManager.GetUserId (User);

            var result = await _mediator.Send (command);

            return result.IsSuccess
                ? (IActionResult)RedirectToAction ("Mine", "Exhibits")
                : (IActionResult)BadRequest(result.Error);
        }
    }
}
