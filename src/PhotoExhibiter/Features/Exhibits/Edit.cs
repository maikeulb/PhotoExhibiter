using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;
using PhotoExhibiter.Infrastructure;

namespace PhotoExhibiter.Features.Exhibits
{
    public class Edit
    {
        public class Query : IRequest<Result<Command>>
        {
            public string UserId { get; set; }
            public int Id { get; set; }
        }

        public class Command : IRequest<Result>
        {
            public int Id { get; set; }

            [Display (Name = "Genre")]
            public int GenreId { get; set; }
            public string UserId { get; set; }
            public string Location { get; set; }
            public string Date { get; set; }
            public string Heading { get; set; }
            public string ImageUrl { get; set; }
            public DateTime DateTime { get; set; }
            public IEnumerable<Genre> Genres { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Result<Command>>
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

            public Result<Command> Handle (Query message)
            {
                var exhibit = _exhibitrepository.GetExhibit (message.Id);

                if (exhibit == null)
                    return Result.Fail<Command> ("Exhibit does not exit");
                if (exhibit.PhotographerId != message.UserId)
                    return Result.Fail<Command> ("Unauthorized");

                var command = new Command
                {
                    Id = exhibit.Id,
                    Heading = "Edit an Exhibit",
                    Genres = _genrerepository.GetGenres (),
                    Date = exhibit.DateTime.ToString ("d MMM yyyy"),
                    ImageUrl = exhibit.ImageUrl,
                    GenreId = exhibit.GenreId,
                    Location = exhibit.Location
                };

                return Result.Ok (command);
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

        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IExhibitRepository _repository;

            public CommandHandler (IExhibitRepository repository)
            {
                _repository = repository;
            }

            public Result Handle (Command message)
            {
                message.DateTime = DateTime.Parse (string.Format ("{0}", message.Date));
                var exhibit = _repository.GetExhibit (message.Id);

                if (exhibit == null)
                    return Result.Fail<Command> ("Exhibit does not exit");
                if (exhibit.PhotographerId != message.UserId)
                    return Result.Fail<Command> ("Unauthorized");

                exhibit.UpdateDetails (message);
                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
