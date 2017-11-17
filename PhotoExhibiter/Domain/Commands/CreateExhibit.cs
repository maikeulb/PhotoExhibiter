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
    public class CreateExhibitQuery : IRequest<CreateExhibitCommand>
    {
        public int? Id { get; set; }
    }

    public class CreateExhibitCommand : IRequest
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string Heading { get; set; }
        public DateTime DateTime { get; set;}
    }

    public class CreateExhibitQueryHandler : IRequestHandler<CreateExhibitQuery, CreateExhibitCommand>
    {
        private readonly IGenreRepository _repository;

        public CreateExhibitQueryHandler (
                IGenreRepository repository) 
        { 
            _repository = repository;
        }

        public CreateExhibitCommand Handle (CreateExhibitQuery message)
        {
            var model = new CreateExhibitCommand
            {
                Genres = _repository.GetGenres(),
                Heading = "Add a Gig"
            };

            return model;
        }
    }

    public class CreateExhibitCommandHandler : IRequestHandler<CreateExhibitCommand>
    {
        private readonly IExhibitRepository _repository;

        public CreateExhibitCommandHandler (
            IExhibitRepository repository)
            {
                _repository = repository;
            }

        public void Handle (CreateExhibitCommand message)
        {
            var exhibit = new Exhibit
            {
                PhotographerId = message.UserId,
                DateTime = DateTime.Parse (string.Format ("{0} {1}", message.Date, message.Time)),
                GenreId = message.Genre,
                Location = message.Location
            };

            _repository.Add (exhibit);
            _repository.SaveAll ();
        }
    }
}
