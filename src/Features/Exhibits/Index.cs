using System.Collections.Generic;
using System.Linq;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Features.Exhibits
{
    public class Index
    {
        public class Query : IRequest<Model>
        {
            public string UserId { get; set; }
            public bool ShowActions { get; set; }
            public string SearchTerm { get; set; }
        }

        public class Model
        {
            public bool ShowActions { get; set; }
            public string Heading { get; set; }
            public string SearchTerm { get; set; }
            public string UserId {get; set; }
            public IEnumerable<Exhibit> UpcomingExhibits { get; set; }
            public IEnumerable<Attendance> Attendances {get; set; } 
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IExhibitRepository _repository;
            private readonly IAttendanceRepository _attendanceRepository;

            public Handler(IExhibitRepository repository,
                           IAttendanceRepository attendanceRepository) 
            {
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
                    Heading = "SEARCH RESULTS",
                    UserId = message.UserId,
                    Attendances = attendances
                };

                return exhibits;
            }
        }
    }
}
