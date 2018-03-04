using System.Collections.Generic;
using System.Linq;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;
using X.PagedList;

namespace PhotoExhibiter.Features.Home
{
    public class Index
    {
        public class Query : IRequest<Model>
        {
            public string UserId { get; set; }
            public string PhotographerId { get; set; }
            public string SearchTerm { get; set; }
            public bool ShowActions { get; set; }
            public int? Page { get; set; }
        }

        public class Model
        {
            public string Heading { get; set; }
            public string SearchTerm { get; set; }
            public string UserId {get; set; }
            public string PhotographerId {get; set; }
            public bool ShowActions { get; set; }
            public IEnumerable<Attendance> Attendances {get; set; } 

            public IPagedList<Exhibit> UpcomingExhibits { get; set; }
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IExhibitRepository _exhibitRepository;
            private readonly IAttendanceRepository _attendanceRepository;

            public Handler(IExhibitRepository exhibitRepository,
                           IAttendanceRepository attendanceRepository) 
            {
                _exhibitRepository = exhibitRepository;
                _attendanceRepository = attendanceRepository;
            }

            public Model Handle (Query message)
            {
                var upcomingExhibits = _exhibitRepository.GetUpcomingExhibits(message.SearchTerm);
                var attendances = _attendanceRepository.GetAllAttendances();

                var model = new Model
                {
                    ShowActions = message.ShowActions,
                    UserId = message.UserId,
                    PhotographerId = message.PhotographerId,
                    Attendances = attendances,
                    Heading = "NYC Photography Exhibits"
                };

                int pageSize = 8;
                int pageNumber = (message.Page ?? 1);
                model.UpcomingExhibits = upcomingExhibits.ToPagedList(pageNumber, pageSize);

                return model;
            }
        }
    }
}
