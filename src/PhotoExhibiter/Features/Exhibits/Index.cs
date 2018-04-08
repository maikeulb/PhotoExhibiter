using System;
using System.Linq;
using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Entities.Interfaces;
using PhotoExhibiter.Infrastructure.Interfaces;

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
            
            public class Exhibit
            {
                public int Id { get; set; }
                public string PhotographerId { get; set; }
                public GenreT Genre { get; set; }
                public string Location { get; set; }
                public string ImageUrl { get;  set; }
                public DateTime DateTime { get; set; }
                public bool IsCanceled { get;  set; }

                public class GenreT
                {
                    public string Name{ get; set; }
                }
            }

            public class Attendance
            {
                public int ExhibitId { get; set; }
                public string AttendeeId { get; set; }
            }
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IExhibitRepository _repository;
            private readonly IAttendanceRepository _attendanceRepository;
            private readonly IUrlComposer _urlComposer;

            public Handler(IExhibitRepository repository,
                           IAttendanceRepository attendanceRepository,
                           IUrlComposer urlComposer)
            {
                _repository = repository;
                _attendanceRepository = attendanceRepository;
                _urlComposer = urlComposer;
            }

            public Model Handle (Query message)
            {
                var upcomingExhibits = _repository.GetUpcomingExhibits(message.SearchTerm);
                var attendances = _attendanceRepository.GetAllAttendances();

                var model = new Model
                {
                    UpcomingExhibits = upcomingExhibits.Select (ue => new Model.Exhibit
                    {
                        Id = ue.Id,
                        PhotographerId = ue.PhotographerId,
                        Location = ue.Location,
                        ImageUrl =  _urlComposer.ComposeImgUrl(ue.ImageUrl),
                        DateTime = ue.DateTime,
                        IsCanceled = ue.IsCanceled,
                        Genre = new Model.Exhibit.GenreT
                                {
                                    Name = ue.Genre.Name 
                                }
                    }).ToList(),
                    Attendances = attendances.Select (a => new Model.Attendance
                    {
                        ExhibitId = a.ExhibitId,
                        AttendeeId = a.AttendeeId
                    }).ToList(),
                    ShowActions = message.ShowActions,
                    UserId = message.UserId,
                    Heading = "SEARCH RESULTS"
                };

                return model;
            }
        }
    }
}
