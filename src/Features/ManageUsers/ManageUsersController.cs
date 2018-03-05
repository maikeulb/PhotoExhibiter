using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Data.Context;
using PhotoExhibiter.Features;
using PhotoExhibiter.Features.Account;
using PhotoExhibiter.Features.Home;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Features.ManageUsers
{
    public class ManageUsersController : Controller
    {
        private readonly IApplicationUserRepository _repository;

        public ManageUsersController (IApplicationUserRepository repository) => _repository = repository;

        public async Task<IActionResult> Index ()
        {
            return View ();
        }

        public ActionResult Details(string id)
        {
            var user = _repository.GetPhotographer(id);

            if (user  == null)
                return NotFound();

            return View(user);
        }

        public ActionResult Edit(string id)
        {
            var user = _repository.GetPhotographer(id);

            if (user == null)
                return NotFound();

            var viewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ImageUrl = user.ImageUrl,
                IsSuspended = user.IsSuspended
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser user)
        {
            var userInDb = _repository.GetPhotographerWithExhibits(user.Id);

            if (userInDb == null)
                return NotFound();

            userInDb.Name = user.Name;
            userInDb.ImageUrl = user.ImageUrl;
            userInDb.IsSuspended = user.IsSuspended;

            if (user.IsSuspended == true)
                userInDb.Suspend();

            _repository.SaveAll();

            return RedirectToAction("Index");
        }
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSuspended { get; set; }
    }
}
