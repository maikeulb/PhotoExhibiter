using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features;

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

        public async Task<IActionResult> Index (Index.Query query)
        {
            query.UserId = _userManager.GetUserId (User);
            if (query.PhotographerId == null)
              query.PhotographerId = query.UserId;
            query.ShowActions = _signInManager.IsSignedIn (User);

            var model = await _mediator.Send (query);

            return View (model);
        }

        [Authorize]
        public async Task<IActionResult> Attending (Index.Query query)
        {
            query.UserId = _userManager.GetUserId (User);
            query.ShowActions = _signInManager.IsSignedIn (User);

            var model = await _mediator.Send (query);

            return View (model);
        }


        public IActionResult About ()
        {
            ViewData["Message"] = "Your application description page.";

            return View ();
        }

        public IActionResult Contact ()
        {
            ViewData["Message"] = "Your contact page.";

            return View ();
        }

        public IActionResult Error ()
        {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
