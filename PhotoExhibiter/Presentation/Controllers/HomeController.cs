using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Presentation.ViewModels;

namespace PhotoExhibiter.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IExhibitRepository _repository;

        public HomeController (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IExhibitRepository repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository= repository;
        }

        public IActionResult Index (string query = null)
        {
            var upcomingExhibits = _repository.GetUpcomingExhibits (query);

            var userId = _userManager.GetUserId (User);

            var viewModel = new ExhibitsViewModel
            {
                UpcomingExhibits = upcomingExhibits,
                ShowActions = _signInManager.IsSignedIn (User),
                Heading = "Upcoming Exhibits"
            };

            return View ("Exhibits", viewModel);
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
