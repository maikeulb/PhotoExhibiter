using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Application.Commands
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
            private readonly IMapper _mapper;

            public Handler (IFollowingRepository repository,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public Result Handle (Command message)
            {
                var following = _repository.GetFollowing (message.UserId, message.FolloweeId);
                if (following != null)
                    return Result.Fail<Command> ("Following already exists.");

                var newFollowing = _mapper.Map<Command, Following> (message);

                _repository.Add (newFollowing);
                _repository.SaveAll ();

                return Result.Ok();
            }
        }
    }
}
