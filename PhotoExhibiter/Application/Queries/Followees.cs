using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Application.Queries
{
    public class Followees
    {
        public class Query : IRequest<IEnumerable<ApplicationUser>>
        {
            public string UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ApplicationUser>>
        {
            private readonly IApplicationUserRepository _repository;

            public Handler (IApplicationUserRepository repository)
            {
                _repository = repository;
            }

            public IEnumerable<ApplicationUser> Handle (Query message)
            {
                var photographers = _repository.GetPhotographersFollowedBy (message.UserId);

                return photographers;
            }
        }
    }
}
