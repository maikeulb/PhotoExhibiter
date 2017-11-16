using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.WebUI.ViewModels;
using PhotoExhibiter.Domain.Queries;

namespace PhotoExhibiter.WebUI.Controllers
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

        public async Task<IActionResult> Index (string query = null)
        {
            var message = new GetExhibitsQuery
            {
                QueryId = query,
                UserId = _userManager.GetUserId (User),
                ShowActions = _signInManager.IsSignedIn (User)
            };

            var model = await _mediator.Send(message);

            return View ("Exhibits", model);
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
