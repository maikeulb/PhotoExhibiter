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

    public class Create
    {
        public class Query : IRequest<Command>
        {
            public int? Id { get; set; }
        }

        public class Command : IRequest
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

        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly IGenreRepository _repository;

            public QueryHandler (
                    IGenreRepository repository) 
            { 
                _repository = repository;
            }

            public Command Handle (Query message)
            {
                var model = new Command
                {
                    Genres = _repository.GetGenres(),
                    Heading = "Add a Exhibit"
                };

                return model;
            }
        }

        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly IExhibitRepository _repository;

            public CommandHandler (
                IExhibitRepository repository)
                {
                    _repository = repository;
                }

            public void Handle (Command message)
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
}
