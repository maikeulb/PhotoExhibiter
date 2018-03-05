using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Features.Home;
using PhotoExhibiter.Features.Account.AccountViewModels;
using PhotoExhibiter.Infrastructure;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Features.Account
{
    [Authorize]
    [Route ("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;

        public AccountController (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login (string returnUrl = null)
        {
            await HttpContext.SignOutAsync (IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View ();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login (LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync (model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                var user = await _userManager.FindByEmailAsync (model.Email);

                if (user.IsSuspended)  
                {
                    return RedirectToAction (nameof (Suspended));
                }
                if (result.Succeeded && !user.IsSuspended)
                {
                    _logger.LogInformation ("User logged in.");
                    return RedirectToLocal (returnUrl);
                }
                else
                {
                    ModelState.AddModelError (string.Empty, "Invalid login attempt.");
                    return View (model);
                }
            }
            return View (model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Suspended ()
        {
            await _signInManager.SignOutAsync ();
            return View ();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register (string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View ();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register (RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name
                };

                var result = await _userManager.CreateAsync (user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation ("User created a new account with password.");
                    await _signInManager.SignInAsync (user, isPersistent : false);
                    _logger.LogInformation ("User created a new account with password.");
                    return RedirectToLocal (returnUrl);
                }
                AddErrors (result);
            }

            return View (model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout ()
        {
            await _signInManager.SignOutAsync ();
            _logger.LogInformation ("User logged out.");
            return RedirectToAction (nameof (HomeController.Index), "Home");
        }


        [HttpGet]
        public IActionResult AccessDenied ()
        {
            return View ();
        }

        #region Helpers

        private void AddErrors (IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError (string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal (string returnUrl)
        {
            if (Url.IsLocalUrl (returnUrl))
            {
                return Redirect (returnUrl);
            }
            else
            {
                return RedirectToAction (nameof (HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
