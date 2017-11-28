using System.Linq;
using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Apis.Attendances
{
    public class UnFollow
    {
        public class Command : IRequest<string>
        {
            public string UserId { get; set; }
            public string FolloweeId { get; set; }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly IFollowingRepository _repository;

            public Handler (IFollowingRepository repository) => _repository = repository;

            public string Handle (Command message)
            {
                var following = _repository.GetFollowing (message.UserId, message.FolloweeId);

                _repository.Remove (following);
                _repository.SaveAll ();

                return message.FolloweeId;
            }
        }
    }
}