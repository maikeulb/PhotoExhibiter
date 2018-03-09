using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Features.Home
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMediator _mediator;

        public HomeController (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mediator = mediator;
        }

        [HttpGet("Index")]
        [HttpGet("")]
        public async Task<IActionResult> Index (Index.Query query)
        {
            query.UserId = _userManager.GetUserId (User);
            if (query.PhotographerId == null)
                query.PhotographerId = query.UserId;
            query.ShowActions = _signInManager.IsSignedIn (User);

            var model = await _mediator.Send (query);

            return View (model);
        }

        public IActionResult Error ()
        {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
