using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.Followings
{
    public class Follow
    {
        public class Command : IRequest<Result>
        {
            public string UserId { get; set; }
            public string FolloweeId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IFollowingRepository _repository;

            public Handler (IFollowingRepository repository) => _repository = repository;

            public Result Handle (Command message)
            {
                var following = _repository.GetFollowing (message.UserId, message.FolloweeId);
                if (following != null)
                    return Result.Fail<Command> ("Following already exists.");

                var newFollowing = Following.Create (message);

                _repository.Add (newFollowing);
                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
