using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Features;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Exhibits
{
    public class Mine
    {
        public class Query : IRequest<IEnumerable<Exhibit>>
        {
            public string UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Exhibit>>
        {
            private readonly IExhibitRepository _repository;

            public Handler (IExhibitRepository repository)
            {
                _repository = repository;
            }

            public IEnumerable<Exhibit> Handle (Query message)
            {
                var exhibits = _repository.GetUpcomingExhibitsByPhotographer (message.UserId);

                return exhibits;
            }
        }
    }
}
