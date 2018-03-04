using System.Linq;
using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.Attendances
{
    public class UnFollow
    {
        public class Command : IRequest<Result<string>>
        {
            public string UserId { get; set; }
            public string FolloweeId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly IFollowingRepository _repository;

            public Handler (IFollowingRepository repository) => _repository = repository;

            public Result<string> Handle (Command message)
            {
                var following = _repository.GetFollowing (message.UserId, message.FolloweeId);
                if (following == null)
                    return Result.Fail<string> ("Following does not exists.");

                _repository.Remove (following);
                _repository.SaveAll ();

                return Result.Ok(message.FolloweeId);
            }
        }
    }
}
