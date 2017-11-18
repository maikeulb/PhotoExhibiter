namespace PhotoExhibiter.Application.Queries
{
    using System.Collections.Generic;
    using MediatR;
    using PhotoExhibiter.Domain.Interfaces;
    using PhotoExhibiter.Domain.Models;

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