using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Features.Home
{
    public class Attending
    {
        public class Query : IRequest<Model>
        {
            public string UserId { get; set; }
            public bool ShowActions { get; set; }
            public string SearchTerm {get; set;}
        }

        public class Model
        {
            private readonly List<Attendance> _attendances = new List<Attendance> ();

            public IEnumerable<Exhibit> UpcomingExhibits { get; set; }
            public bool ShowActions { get; set; }
            public string Heading { get; set; }
            public string SearchTerm { get; set; }

            public IEnumerable<Attendance> Attendances => _attendances.AsReadOnly ();
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IExhibitRepository _repository;

            public Handler(IExhibitRepository repository) => _repository = repository;

            public Model Handle (Query message)
            {
                var upcomingExhibits = _repository.GetExhibitsUserAttending(message.UserId);

                var exhibits = new Model
                {
                    UpcomingExhibits = upcomingExhibits,
                    ShowActions = message.ShowActions,
                    Heading = "Exhibits I'm Attending"
                };

                return exhibits;
            }
        }
    }
}
