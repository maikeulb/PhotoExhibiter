using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using PhotoExhibiter.Features;
using PhotoExhibiter.Infra.App;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

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
            public int GenreId { get; set; }
            public string UserId { get; set; }
            public string Location { get; set; }
            public string Date { get; set; }
            public string Heading { get; set; }
            public string ImageUrl { get; set; }
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
                var model = new Command
                {
                    UserId = message.UserId,
                    Genres = _repository.GetGenres (),
                    Heading = "Add a Exhibit"
                };

                return model;
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator ()
            {
                RuleFor (m => m.Location)
                    .NotNull ().WithMessage("Name is required.")
                    .Length(1,100).WithMessage("Length must be between 1 and 100 characters");
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

            public CommandHandler(IExhibitRepository repository) => _repository = repository;

            public void Handle (Command message)
            {
                message.DateTime = DateTime.Parse(string.Format("{0}", message.Date));

                var exhibit = Exhibit.Create(message);

                _repository.Add (exhibit);
                _repository.SaveAll ();
            }
        }
    }
}
