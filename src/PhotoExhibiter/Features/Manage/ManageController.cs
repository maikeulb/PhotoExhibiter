﻿using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Manage.ManageViewModels;
using PhotoExhibiter.Infrastructure;
using PhotoExhibiter.Data;

namespace PhotoExhibiter.Features.Manage
{
    [Authorize]
    [Route ("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;

        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageController (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ManageController> logger,
            UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _urlEncoder = urlEncoder;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index ()
        {
            var user = await _userManager.GetUserAsync (User);
            if (user == null)
            {
                throw new ApplicationException ($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View (model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index (IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View (model);
            }

            var user = await _userManager.GetUserAsync (User);
            if (user == null)
            {
                throw new ApplicationException ($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction (nameof (Index));
        }


        [HttpGet]
        public async Task<IActionResult> SetPassword ()
        {
            var user = await _userManager.GetUserAsync (User);
            if (user == null)
            {
                throw new ApplicationException ($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync (user);

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View (model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword (SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View (model);
            }

            var user = await _userManager.GetUserAsync (User);
            if (user == null)
            {
                throw new ApplicationException ($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync (user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors (addPasswordResult);
                return View (model);
            }

            await _signInManager.SignInAsync (user, isPersistent : false);
            StatusMessage = "Your password has been set.";

            return RedirectToAction (nameof (SetPassword));
        }

        #region Helpers

        private void AddErrors (IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError (string.Empty, error.Description);
            }
        }

        private string FormatKey (string unformattedKey)
        {
            var result = new StringBuilder ();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append (unformattedKey.Substring (currentPosition, 4)).Append (" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append (unformattedKey.Substring (currentPosition));
            }

            return result.ToString ().ToLowerInvariant ();
        }

        private string GenerateQrCodeUri (string email, string unformattedKey)
        {
            return string.Format (
                AuthenicatorUriFormat,
                _urlEncoder.Encode ("Web"),
                _urlEncoder.Encode (email),
                unformattedKey);
        }

        #endregion
    }
}
