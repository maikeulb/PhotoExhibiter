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
using PhotoExhibiter.Models.Entities;

namespace PhotoExhibiter.Features.Users
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public UsersController (
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

        public async Task<IActionResult> Index (Index.Query query)
        {
            query.UserId = _userManager.GetUserId (User);
            query.ShowActions = _signInManager.IsSignedIn (User);
            var photographerProfile = await _userManager.FindByIdAsync (query.PhotographerId);
            query.PhotographerName = photographerProfile.Name;

            var model = await _mediator.Send (query);

            return View (model);
        }

        [HttpPost]
        public IActionResult Search (Index.Query model)
        {
            return RedirectToAction ("Index", "Exhibits", model);
        }

    }
}
