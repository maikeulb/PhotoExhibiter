using AutoMapper;
using MediatR;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Application.Commands
{
    public class Follow
    {
        public class Command : IRequest<CommandResult>
        {
            public string UserId { get; set; }
            public string FolloweeId { get; set; }
        }

        public class CommandResult
        {
            private CommandResult () { }

            private CommandResult (string failureReason)
            {
                FailureReason = failureReason;
            }

            public string FailureReason { get; }

            public bool IsSuccess => string.IsNullOrEmpty (FailureReason);

            public static CommandResult Success { get; } = new CommandResult ();

            public static CommandResult Fail (string reason)
            {
                return new CommandResult (reason);
            }

            public static implicit operator bool (CommandResult result)
            {
                return result.IsSuccess;
            }
        }

        public class Handler : IRequestHandler<Command, CommandResult>
        {
            private readonly IFollowingRepository _repository;
            private readonly IMapper _mapper;

            public Handler (IFollowingRepository repository,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public CommandResult Handle (Command message)
            {
                var following = _repository.GetFollowing (message.UserId, message.FolloweeId);
                if (following != null)
                    return CommandResult.Fail ("Following already exists.");

                var newFollowing = _mapper.Map<Command, Following> (message);
                _repository.Add (newFollowing);
                _repository.SaveAll ();
                return CommandResult.Success;
            }
        }
    }
}
