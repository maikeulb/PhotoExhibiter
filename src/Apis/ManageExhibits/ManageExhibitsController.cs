using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Data.Context;
using PhotoExhibiter.Apis.Attendances;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.ManageExhibits
{
    [Route ("api/[Controller]")]
    public class ManageExhibitsController : Controller
    {
        private readonly IExhibitRepository _repository;

        public ManageExhibitsController (IExhibitRepository repository) => _repository = repository;

        [Authorize(Roles="Admin, DemoAdmin")]
        public IActionResult GetExhibits (string query = null)
        {
            var exhibitsInDb = _repository.GetAllExhibits(query);

            var exhibitsDto = exhibitsInDb.Select( e =>  new ExhibitDto()
            {
                Id = e.Id,
                Genre = e.Genre.Name,
                Photographer = e.Photographer.Name,
                Date = e.DateTime.ToString ("d MMM yyyy"),
                Location = e.Location,
                ImageUrl = e.ImageUrl,
                DateTime = e.DateTime,
                IsCanceled = e.IsCanceled,
            });
            
            return Ok(exhibitsDto);    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public IActionResult EditExhibit(int id, ExhibitDto exhibitDto)
        {
            var exhibitInDb = _repository.GetExhibit(id);

            if (exhibitInDb == null)
                return NotFound();

            exhibitInDb.ManagerUpdate( 
                    exhibitDto.Location,
                    exhibitDto.DateTime,
                    exhibitDto.ImageUrl,
                    exhibitDto.GenreId);
            
            if (exhibitDto.IsCanceled == true)
                exhibitInDb.Cancel();

            _repository.SaveAll();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles="Admin")]
        public IActionResult Cancel([FromBody]CancelDto command)
        {
            var exhibitInDb = _repository.GetExhibitWithAttendees (command.Id);

            if (exhibitInDb == null)
                return NotFound();

            exhibitInDb.Cancel();
            _repository.SaveAll();

            return Ok();
        }
    }

    public class ExhibitDto
    {
        public int Id { get; set; }
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

    public class CancelDto
    {
        public int Id { get; set; }
    }
}
