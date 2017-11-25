using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Apis.Attendances
{
    public class Attend
    {
        public class Command : IRequest<Result>
        {
            public string UserId { get; set; }
            public int ExhibitId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IAttendanceRepository _repository;

            public Handler (IAttendanceRepository repository)
            {
                _repository = repository;
            }

            public Result Handle (Command message)
            {
                var attendance = _repository.GetAttendance (message.ExhibitId, message.UserId);
                if (attendance != null)
                    return Result.Fail<Command> ("Attendance already exists.");

                var newAttendance = Attendance.Create(message);
                _repository.Add (newAttendance);
                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
