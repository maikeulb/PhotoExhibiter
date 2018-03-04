using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Features;
using PhotoExhibiter.Features.Account;
using PhotoExhibiter.Features.Home;
using PhotoExhibiter.Entities;

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

        /* [AllowAnonymous] */
        /* public async Task<IActionResult> Details (Details.Query query) */
        /* { */
        /*     query.UserId = _userManager.GetUserId (User); */

        /*     var model = await _mediator.Send (query); */

        /*     return View (model); */
        /* } */

        /* public async Task<IActionResult> Mine (Mine.Query query) */
        /* { */
        /*     query.UserId = _userManager.GetUserId (User); */
        /*     query.ShowActions = _signInManager.IsSignedIn (User); */
        /*     var model = await _mediator.Send (query); */

            /* return View (model); */
        /* } */

        /* public async Task<IActionResult> Attending (Attending.Query query) */
        /* { */
        /*     query.UserId = _userManager.GetUserId (User); */
        /*     query.ShowActions = _signInManager.IsSignedIn (User); */

/*             var model = await _mediator.Send (query); */

/*             return View (model); */
/*         } */

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

            return modelOrError.IsSuccess
                ? (IActionResult)View(modelOrError.Value)
                : (IActionResult)BadRequest(modelOrError.Error);
        }

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
