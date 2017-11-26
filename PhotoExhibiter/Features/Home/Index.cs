using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Home
{
    public class Index
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
                var exhibits = new Model
                {
                    UpcomingExhibits = _repository.GetUpcomingExhibits (),
                    ShowActions = message.ShowActions,
                    Heading = "Upcoming Exhibits"
                };

                return exhibits;
            }
        }
    }
}
