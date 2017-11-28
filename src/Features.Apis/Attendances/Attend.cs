using System.Linq;
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
            private readonly IExhibitRepository _repository;
            private readonly IAttendanceRepository _attendanceRepository;

            public Handler (IExhibitRepository repository, IAttendanceRepository attendanceRepository)
            {
                _repository = repository;
                _attendanceRepository = attendanceRepository;
            }

            public Result Handle (Command message)
            {
                /* var attendance = _repository.GetAttendance (message.ExhibitId, message.UserId); */
                /* if (attendance != null) */
                /* return Result.Fail<Command> ("Attendance already exists."); */
                /* var newAttendance = Attendance.Create(message); */
                /* _repository.AddAttendance (newAttendance); */

                var exhibit = _repository.GetExhibit (message.ExhibitId);
                /* var attendance = _attendancerepository.GetAttendance (message.ExhibitId, message.UserId); */
                /* if (attendance != null) */
                /* return Result.Fail<Command> ("Attendance already exists."); */

                /* var contains = exhibit.Attendances.Any(a => a.AttendeeId == message.UserId); */
                var contains = exhibit.Attendances.Any (a => a.AttendeeId == message.UserId);
                if (contains == true)
                    return Result.Fail<Command> ("Attendance already exists.");

                exhibit.AddAttendance (Attendance.Create (message));

                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}