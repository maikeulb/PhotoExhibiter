using System.Linq;
using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.Attendances
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
            private readonly IExhibitRepository _exhibitRepository;

            public Handler (IExhibitRepository exhibitRepository)
            {
                _exhibitRepository = exhibitRepository;
            }

            public Result Handle (Command message)
            {
                var exhibit = _exhibitRepository.GetExhibit (message.ExhibitId);
                if (exhibit == null)
                    return Result.Fail<int> ("Exhibit does not exit");

                var contains = exhibit.Attendances.Any (a => a.AttendeeId == message.UserId);
                if (contains == true)
                    return Result.Fail<Command> ("Attendance already exists.");

                exhibit.AddAttendance (Attendance.Create (message));
                _exhibitRepository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}