using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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

namespace PhotoExhibiter.Features.ManageExhibits
{
    public class ManageExhibitsController : Controller
    {
        private readonly IExhibitRepository _repository;

        public ManageExhibitsController (IExhibitRepository repository) => _repository = repository;

        [Authorize(Roles="Admin, DemoAdmin")]
        public async Task<IActionResult> Index ()
        {
            return View ();
        }

        [Authorize(Roles="Admin, DemoAdmin")]
        public ActionResult Details(int id)
        {
            var exhibit = _repository.GetExhibit(id);

            if (exhibit  == null)
                return NotFound();

            var viewModel = new ExhibitViewModel
            {
                Id = exhibit.Id,
                Genre = exhibit.Genre.Name,
                Photographer = exhibit.Photographer.Name,
                Date = exhibit.DateTime.ToString ("d MMM yyyy"),
                Location = exhibit.Location,
                ImageUrl = exhibit.ImageUrl,
                DateTime = exhibit.DateTime,
                IsCanceled = exhibit.IsCanceled,
            };

            return View(viewModel);
        }

        [Authorize(Roles="Admin, DemoAdmin")]
        public ActionResult Edit(int id)
        {
            var exhibit = _repository.GetExhibit(id);

            if (exhibit == null)
                return NotFound();

            var viewModel = new ExhibitViewModel
            {
                Id = exhibit.Id,
                Genre = exhibit.Genre.Name,
                Photographer = exhibit.Photographer.Name,
                Location = exhibit.Location,
                Date = exhibit.DateTime.ToString ("d MMM yyyy"),
                ImageUrl = exhibit.ImageUrl,
                DateTime = exhibit.DateTime,
                IsCanceled = exhibit.IsCanceled,
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public ActionResult Edit(Exhibit exhibit)
        {
            var exhibitInDb = _repository.GetExhibit(exhibit.Id);

            exhibitInDb.ManagerUpdate( 
                    exhibit.Location,
                    exhibit.DateTime,
                    exhibit.ImageUrl,
                    exhibit.GenreId);
            
            if (exhibit.IsCanceled == true)
                exhibitInDb.Cancel();

            _repository.SaveAll();

            return RedirectToAction("Index");
        }
    }

    public class ExhibitViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        public string Date { get; set; }
        public string Genre { get; set; }
        public string UserId { get; set; }
        public string Location { get; set; }
        public string Photographer { get; set; }
        public string ImageUrl { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
