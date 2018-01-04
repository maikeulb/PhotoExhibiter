using System.Collections.Generic;
using System.Linq;
using MediatR;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Home
{
    public class Index
    {
        public class Query : IRequest<Model>
        {
            public string SearchTerm { get; set; }
            public string UserId { get; set; }
            public bool ShowActions { get; set; }
        }

        public class Model
        {
            public IEnumerable<Exhibit> UpcomingExhibits { get; set; }
            public bool ShowActions { get; set; }
            public string Heading { get; set; }
            public string SearchTerm { get; set; }
            public IEnumerable<Attendance> Attendances {get; set; } 
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IExhibitRepository _repository;
            private readonly IAttendanceRepository _attendanceRepository;

            public Handler(IExhibitRepository repository,
                           IAttendanceRepository attendanceRepository) {

                _repository = repository;
                _attendanceRepository = attendanceRepository;
            }

            public Model Handle (Query message)
            {
                var upcomingExhibits = _repository.GetUpcomingExhibits(message.SearchTerm);
                var attendances = _attendanceRepository.GetAllAttendances();

                var exhibits = new Model
                {
                    UpcomingExhibits = upcomingExhibits,
                    ShowActions = message.ShowActions,
                    Heading = "NYC Photography Exhibits",
                    Attendances = attendances
                };

                return exhibits;
            }
        }
    }
}
