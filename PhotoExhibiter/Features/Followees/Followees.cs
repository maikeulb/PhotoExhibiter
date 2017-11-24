using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Followees
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
