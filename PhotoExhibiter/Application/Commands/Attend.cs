using AutoMapper;
using MediatR;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Application.Commands
{
    public class Attend
    {
        public class Command : IRequest
        {
            public string UserId { get; set; }
            public int ExhibitId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IAttendanceRepository _repository;
            private readonly IMapper _mapper;

            public Handler (IAttendanceRepository repository,
                IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public void Handle (Command message)
            {
                var attendance = _mapper.Map<Command, Attendance> (message);

                _repository.Add (attendance);
                _repository.SaveAll ();
            }
        }
    }
}
