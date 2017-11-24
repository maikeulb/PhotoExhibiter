using System;
using System.Collections.Generic;
using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using PhotoExhibiter.Features;
using PhotoExhibiter.Infra.App;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

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
            public string UserId { get; set; }
            public int Id { get; set; }
            public string Location { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public int GenreId { get; set; }
            public IEnumerable<Genre> Genres { get; set; }
            public string Heading { get; set; }
            public DateTime DateTime { get; set; }
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

                var model = new Command
                {
                    Id = exhibit.Id,
                    Location = exhibit.Location,
                    Date = exhibit.DateTime.ToString ("d MMM yyyy"),
                    Time = exhibit.DateTime.ToString ("HH:mm"),
                    GenreId = exhibit.GenreId,
                    Genres = _genrerepository.GetGenres (),
                    Heading = "Edit a Exhibit",
                };

                return Result.Ok (model);
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator ()
            {
                RuleFor (m => m.Location).NotNull ();
                RuleFor (m => m.Date).NotNull ().SetValidator (new FutureDateValidator ());
                RuleFor (m => m.Time).NotNull ().SetValidator (new ValidTimeValidator ());
                RuleFor (m => m.GenreId).NotNull ();
            }
        }

        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly IExhibitRepository _repository;
            private readonly IMapper _mapper;

            public CommandHandler (
                IExhibitRepository repository,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public Result Handle (Command message)
            {
                var exhibit = _repository.GetExhibit (message.Id);
                if (exhibit == null)
                    return Result.Fail<Command> ("Exhibit does not exit");
                if (exhibit.PhotographerId != message.UserId)
                    return Result.Fail<Command> ("Unauthorized");

                var model = _mapper.Map<Command, Exhibit> (message);

                exhibit.UpdateDetails (model.DateTime, model.Location, model.GenreId);
                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
