namespace PhotoExhibiter.Application.Commands
{
    using System.Collections.Generic;
    using System;
    using AutoMapper;
    using MediatR;
    using PhotoExhibiter.Domain.Interfaces;
    using PhotoExhibiter.Domain.Models;

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