using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoExhibiter.Domain.Commands;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Queries;
using PhotoExhibiter.WebUI.Controllers;
using PhotoExhibiter.WebUI.ViewModels;

namespace PhotoExhibiter.Domain.Commands
{
    public class EditExhibitQuery : IRequest<EditExhibitCommand>
    {
        public string UserId { get; set;}
        public int Id { get; set; }
    }

    public class EditExhibitCommand : IRequest
    {
        public string UserId { get; set;}
        public int Id { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Genre { get; set; } 
        public IEnumerable<Genre> Genres { get; set; } 
        public string Heading { get; set;} 
        public DateTime DateTime { get; set;}
    }

    public class EditExhibitQueryHandler : IRequestHandler<EditExhibitQuery, EditExhibitCommand>
    {
        private readonly IExhibitRepository _exhibitrepository;
        private readonly IGenreRepository _genrerepository;

        public EditExhibitQueryHandler (
            IExhibitRepository exhibitrepository,
            IGenreRepository genrerepository)
        {
            _exhibitrepository = exhibitrepository;
            _genrerepository = genrerepository;
        }

        public EditExhibitCommand Handle(EditExhibitQuery message)
        {
            var exhibit = _exhibitrepository.GetExhibit(message.Id);

            var model = new EditExhibitCommand
            {
                Heading = "Edit a Gig",
                Id = exhibit.Id,
                Genres = _genrerepository.GetGenres(),
                Date = exhibit.DateTime.ToString("d MMM yyyy"),
                Time = exhibit.DateTime.ToString("HH:mm"),
                Genre = exhibit.GenreId,
                Location = exhibit.Location
            };

            return model;
        }
    }

    public class EditExhibitCommandHandler : IRequestHandler<EditExhibitCommand>
    {
        private readonly IExhibitRepository _repository;

        public EditExhibitCommandHandler(IExhibitRepository repository)
        {
            _repository = repository;
        }

        public void Handle(EditExhibitCommand message)
        {
            var exhibit = _repository.GetExhibitWithAttendees (message.Id);

            var model = new EditExhibitCommand
            {
                DateTime = DateTime.Parse (string.Format ("{0} {1}", message.Date, message.Time)),
                Location = message.Location,
                Genre = message.Genre,
            };

            exhibit.Modify (model.DateTime, model.Location, model.Genre);
            _repository.SaveAll ();
        }
    }
}
