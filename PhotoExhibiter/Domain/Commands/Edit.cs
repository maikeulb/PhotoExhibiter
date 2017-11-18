namespace PhotoExhibiter.Domain.Commands
{
    using AutoMapper;
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
            public int GenreId { get; set; } 
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
                    Id = exhibit.Id,
                    Location = exhibit.Location,
                    Date = exhibit.DateTime.ToString("d MMM yyyy"),
                    Time = exhibit.DateTime.ToString("HH:mm"),
                    GenreId = exhibit.GenreId,
                    Genres = _genrerepository.GetGenres(),
                    Heading = "Edit a Exhibit",
                };

                return model;
            }
        }

        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly IExhibitRepository _repository;
            private readonly IMapper _mapper;

            public CommandHandler(
                    IExhibitRepository repository,
                    IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public void Handle(Command message)
            {
                var exhibit = _repository.GetExhibitWithAttendees (message.Id);

                var model = _mapper.Map<Command, Exhibit>(message);

                exhibit.Modify (model.DateTime, model.Location, model.GenreId);
                _repository.SaveAll ();
            }
        }
    }
}
