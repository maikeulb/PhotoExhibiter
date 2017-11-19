using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using MediatR;
using PhotoExhibiter.Application;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Application.Commands
{
    public class Edit
    {
        public class Query : IRequest<Command>
        {
            public int Id { get; set; }
        }

        public class Command : IRequest
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

            public Command Handle (Query message)
            {
                var exhibit = _exhibitrepository.GetExhibit (message.Id);

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

                return model;
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

        public class CommandHandler : IRequestHandler<Command>
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

            public void Handle (Command message)
            {
                var exhibit = _repository.GetExhibitWithAttendees (message.Id);

                var model = _mapper.Map<Command, Exhibit> (message);

                exhibit.Modify (model.DateTime, model.Location, model.GenreId);
                _repository.SaveAll ();
            }
        }
    }
}
