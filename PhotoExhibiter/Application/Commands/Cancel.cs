using MediatR;
using PhotoExhibiter.Domain.Interfaces;

namespace PhotoExhibiter.Application.Commands
{
    public class Cancel
    {
        public class Command : IRequest<CommandResult>
        {
            public string UserId { get; set; }
            public int Id { get; set; }
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
            private readonly IExhibitRepository _repository;

            public Handler (IExhibitRepository repository)
            {
                _repository = repository;
            }

            public CommandResult Handle (Command message)
            {
                var exhibit = _repository.GetExhibitWithAttendees (message.Id);
                if (exhibit == null)
                    return CommandResult.Fail ("Exhibit does not exist");
                if (exhibit.IsCanceled)
                    return CommandResult.Fail ("Exhibit is cancelled.");
                if (exhibit.PhotographerId != message.UserId)
                    return CommandResult.Fail ("Unauthorized");

                exhibit.Cancel ();
                _repository.SaveAll ();

                return CommandResult.Success;
            }
        }
    }
}
