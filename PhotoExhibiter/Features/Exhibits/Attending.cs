using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Features;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Exhibits
{
    public class Attending
    {
        public class Query : IRequest<Model>
        {
            public string UserId { get; set; }
            public bool ShowActions { get; set; }
        }

        public class Model
        {
            public IEnumerable<Exhibit> UpcomingExhibits { get; set; }
            public bool ShowActions { get; set; }
            public string Heading { get; set; }
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IExhibitRepository _repository;

            public Handler(IExhibitRepository repository) => _repository = repository;

            public Model Handle (Query message)
            {
                var upcomingExhibits = _repository.GetExhibitsUserAttending (message.UserId);
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