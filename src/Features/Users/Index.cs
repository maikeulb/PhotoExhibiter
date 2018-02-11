using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Features;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Users
{
    public class Index
    {
        public class Query : IRequest<Model>
        {
            public string UserId { get; set; }
            public string PhotographerId { get; set; }
            public bool ShowActions { get; set; }
            public string SearchTerm {get; set;}
        }

        public class Model
        {
            private readonly List<Attendance> _attendances = new List<Attendance> ();

            public string UserId {get; set; }
            public string PhotographerId {get; set; }
            public string Heading { get; set; }
            public string SearchTerm { get; set; }
            public bool ShowActions { get; set; }
            public IEnumerable<Exhibit> UpcomingExhibits { get; set; }
            public IEnumerable<Attendance> Attendances => _attendances.AsReadOnly ();
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IExhibitRepository _repository;

            public Handler(IExhibitRepository repository) => _repository = repository;

            public Model Handle (Query message)
            {
                var upcomingExhibits = _repository.GetUpcomingExhibitsByPhotographer (message.PhotographerId);
                var exhibits = new Model
                {
                    PhotographerId = message.PhotographerId,
                    UserId = message.UserId,
                    UpcomingExhibits = upcomingExhibits,
                    ShowActions = message.ShowActions,
                    Heading = "[Users] Exhibits"
                };
                return exhibits;
            }
        }
    }
}
