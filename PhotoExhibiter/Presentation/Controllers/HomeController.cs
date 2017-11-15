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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController (UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index (string query = null)
        {
            var upcomingExhibits = _unitOfWork.Exhibits.GetUpcomingExhibits (query);
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