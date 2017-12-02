using System.Linq;
using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Apis.Attendances
{
    public class Cancel
    {
        public class Command : IRequest<int>
        {
            public string UserId { get; set; }
            public int ExhibitId { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IExhibitRepository _repository;
            private readonly IAttendanceRepository _attendanceRepository;

            public Handler (IExhibitRepository repository, IAttendanceRepository attendanceRepository)
            {
                _repository = repository;
                _attendanceRepository = attendanceRepository;
            }

            public int Handle (Command message)
            {
                var exhibit = _repository.GetExhibit (message.ExhibitId);
                var attendance = exhibit.Attendances.FirstOrDefault (a => a.AttendeeId == message.UserId);

                exhibit.RemoveAttendance (attendance);
                _repository.SaveAll ();
                _attendanceRepository.SaveAll ();

                return message.ExhibitId;
            }
        }
    }
}
