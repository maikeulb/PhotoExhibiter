using AutoMapper;
using MediatR;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Application.Commands
{
    public class Attend
    {
        public class Command : IRequest<CommandResult>
        {
            public string UserId { get; set; }
            public int ExhibitId { get; set; }
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
            private readonly IAttendanceRepository _repository;
            private readonly IMapper _mapper;

            public Handler (IAttendanceRepository repository,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public CommandResult Handle (Command message)
            {
                var attendance = _repository.GetAttendance (message.ExhibitId, message.UserId);
                if (attendance != null)
                    return CommandResult.Fail ("Attendance already exists.");

                var newAttendance = _mapper.Map<Command, Attendance> (message);
                _repository.Add (newAttendance);
                _repository.SaveAll ();

                return CommandResult.Success;
            }
        }
    }
}
