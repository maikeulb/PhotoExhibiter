using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoExhibiter.Data;
using PhotoExhibiter.Models;

namespace PhotoExhibiter.Controllers
{
    public class FolloweesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public FolloweesController (ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index ()
        {
            var userId = _userManager.GetUserId (User);
            var photographers = _context.Followings
                .Where (f => f.FollowerId == userId)
                .Select (f => f.Followee)
                .ToList ();

            return View (photographers);
        }
    }
}