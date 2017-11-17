namespace PhotoExhibiter.Domain.Commands
{
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
    using PhotoExhibiter.Domain.Models;
    using PhotoExhibiter.Domain.Interfaces;
    using PhotoExhibiter.Domain.Queries;
    using PhotoExhibiter.WebUI.Controllers;
    using PhotoExhibiter.WebUI.ViewModels;

    public class Edit
    {
        public class Query : IRequest<Command>
        {
            public int Id { get; set; }
        }

        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Location { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public int Genre { get; set; } 
            public IEnumerable<Genre> Genres { get; set; } 
            public string Heading { get; set;} 
            public DateTime DateTime { get; set;}
        }

        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly IExhibitRepository _exhibitrepository;
            private readonly IGenreRepository _genrerepository;

            public QueryHandler (
                IExhibitRepository exhibitrepository,
                IGenreRepository genrerepository)
            {
                _exhibitrepository = exhibitrepository;
                _genrerepository = genrerepository;
            }

            public Command Handle(Query message)
            {
                var exhibit = _exhibitrepository.GetExhibit(message.Id);

                var model = new Command
                {
                    Heading = "Edit a Exhibit",
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

        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly IExhibitRepository _repository;

            public CommandHandler(IExhibitRepository repository)
            {
                _repository = repository;
            }

            public void Handle(Command message)
            {
                var exhibit = _repository.GetExhibitWithAttendees (message.Id);

                var model = new Command
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
}
