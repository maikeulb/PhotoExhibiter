using System.Linq;
using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.Attendances
{
    public class Cancel
    {
        public class Command : IRequest<Result<int>>
        {
            public string UserId { get; set; }
            public int ExhibitId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly IExhibitRepository _exhibitRepository;

            public Handler (IExhibitRepository exhibitRepository)
            {
                _exhibitRepository = exhibitRepository;
            }

            public Result<int> Handle (Command message)
            {
                var exhibit = _exhibitRepository.GetExhibit (message.ExhibitId);
                if (exhibit == null)
                    return Result.Fail<int> ("Exhibit does not exit");
                var attendance = exhibit.Attendances.FirstOrDefault (a => a.AttendeeId == message.UserId);
                if (attendance == null)
                    return Result.Fail<int> ("Attendance does not exit");

                exhibit.RemoveAttendance (attendance);
                _exhibitRepository.SaveAll ();

                return Result.Ok(message.ExhibitId);
            }
        }
    }
}
