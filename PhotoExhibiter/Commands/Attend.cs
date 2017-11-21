using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;

namespace PhotoExhibiter.Commands
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
            private readonly IMapper _mapper;

            public Handler (IAttendanceRepository repository,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public Result Handle (Command message)
            {
                var attendance = _repository.GetAttendance (message.ExhibitId, message.UserId);
                if (attendance != null)
                    return Result.Fail<Command> ("Attendance already exists.");

                var newAttendance = _mapper.Map<Command, Attendance> (message);

                _repository.Add (newAttendance);
                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}