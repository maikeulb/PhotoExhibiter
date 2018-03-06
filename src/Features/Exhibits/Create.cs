using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;
using PhotoExhibiter.Infrastructure;

namespace PhotoExhibiter.Features.Exhibits
{
    public class Create
    {
        public class Query : IRequest<Command>
        {
            public string UserId { get; set; }
        }

        public class Command : IRequest
        {
            public int Id { get; set; }

            [Display (Name = "Genre")]
            public int GenreId { get; set; }
            public string UserId { get; set; }
            public string Location { get; set; }
            public string Date { get; set; }
            public string Heading { get; set; }
            public string ImageUrl { get; set; }

            [Display (Name = "Date")]
            public DateTime DateTime { get; set; }
            public IEnumerable<Genre> Genres { get; set; }
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
                var command = new Command
                {
                    UserId = message.UserId,
                    Genres = _repository.GetGenres (),
                    Heading = "Add an Exhibit"
                };

                return command;
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator ()
            {
                RuleFor (m => m.Location)
                    .NotNull ().WithMessage ("Name is required.")
                    .Length (1, 100).WithMessage ("Length must be between 1 and 100 characters");
                RuleFor (m => m.Date)
                    .NotNull ()
                    .SetValidator (new FutureDateValidator ());
                RuleFor (m => m.GenreId)
                    .NotNull ();
                RuleFor (m => m.ImageUrl)
                    .NotNull ();
            }
        }

        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly IExhibitRepository _repository;

            public CommandHandler (IExhibitRepository repository) => _repository = repository;

            public void Handle (Command message)
            {
                message.DateTime = DateTime.Parse (string.Format ("{0}", message.Date));

                var exhibit = Exhibit.Create (message);

                _repository.Add (exhibit);
                _repository.SaveAll ();
            }
        }
    }
}