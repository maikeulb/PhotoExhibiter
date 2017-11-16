using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Handlers;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Presentation.ViewModels;

namespace PhotoExhibiter.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMediator _mediatr;

        public HomeController (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMediator mediatr)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mediatr = mediatr;
        }

        public async Task<IActionResult> Index (string query = null)
        {
            var exhibitsquery = new ExhibitsQuery
            {
                QueryId = query,
                UserId = _userManager.GetUserId (User),
                ShowActions = _signInManager.IsSignedIn (User)
            };

            ExhibitsViewModel viewmodel = await _mediatr.Send(exhibitsquery);

            return View ("Exhibits", viewmodel);
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
