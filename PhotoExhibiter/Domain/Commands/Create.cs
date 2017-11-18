namespace PhotoExhibiter.Domain.Commands
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System;
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using PhotoExhibiter.Domain.Commands;
    using PhotoExhibiter.Domain.Interfaces;
    using PhotoExhibiter.Domain.Models;
    using PhotoExhibiter.Domain.Queries;
    using PhotoExhibiter.WebUI.Controllers;
    using PhotoExhibiter.WebUI.ViewModels;

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
