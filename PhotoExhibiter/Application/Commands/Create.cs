using System.Collections.Generic;
using System;
using FluentValidation;
using AutoMapper;
using MediatR;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Application.Commands
{
    public class Create
    {
        public class Query : IRequest<Command>
        {
            public string UserId { get; set; }
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
            public Validator()
            {
                RuleFor(m => m.Location).NotNull();
                RuleFor(m => m.Date).NotNull().NotEmpty(); //additional
                RuleFor(m => m.Time).NotNull().NotEmpty(); //
                RuleFor(m => m.GenreId).NotNull(); //
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
                var exhibit = _mapper.Map<Command, Exhibit> (message);

                _repository.Add (exhibit);
                _repository.SaveAll ();
            }
        }
    }
}
