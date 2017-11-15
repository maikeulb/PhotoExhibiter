using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;

namespace PhotoExhibiter.Presentation.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public FolloweesController (UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index ()
        {
            var userId = _userManager.GetUserId (User);
            var photographers = _unitOfWork.Users.GetPhotographersFollowedBy (userId);

            return View (photographers);
        }
    }
}