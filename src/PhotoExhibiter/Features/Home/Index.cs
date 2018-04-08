using System;
using System.Linq;
using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Entities.Interfaces;
using PhotoExhibiter.Infrastructure.Interfaces;
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

            public class Exhibit
            {
                public int Id { get; set; }
                public string PhotographerId { get; set; }
                public PhotographerT Photographer { get;  set; }
                public GenreT Genre { get; set; }
                public string Location { get; set; }
                public string ImageUrl { get;  set; }
                public DateTime DateTime { get; set; }
                public bool IsCanceled { get;  set; }

                public class PhotographerT
                {
                    public string Name { get; set; }
                }
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
            private readonly IExhibitRepository _exhibitRepository;
            private readonly IAttendanceRepository _attendanceRepository;
            private readonly IUrlComposer _urlComposer;

            public Handler(IExhibitRepository exhibitRepository,
                           IAttendanceRepository attendanceRepository,
                           IUrlComposer urlComposer)
            {
                _exhibitRepository = exhibitRepository;
                _attendanceRepository = attendanceRepository;
                _urlComposer = urlComposer;
            }

            public Model Handle (Query message)
            {
                var upcomingExhibits = _exhibitRepository.GetUpcomingExhibits(message.SearchTerm);
                var attendances = _attendanceRepository.GetAllAttendances();

                var model = new Model
                {
                    Attendances = attendances.Select (a => new Model.Attendance
                    {
                        ExhibitId = a.ExhibitId,
                        AttendeeId = a.AttendeeId
                    }).ToList(),
                    ShowActions = message.ShowActions,
                    UserId = message.UserId,
                    PhotographerId = message.PhotographerId,
                    Heading = "NYC Photography Exhibits"
                };

                int pageSize = 8;
                int pageNumber = (message.Page ?? 1);

                model.UpcomingExhibits = upcomingExhibits.Select (ue => new Model.Exhibit
                    {
                        Id = ue.Id,
                        PhotographerId = ue.PhotographerId,
                        Location = ue.Location,
                        ImageUrl = _urlComposer.ComposeImgUrl(ue.ImageUrl),
                        DateTime = ue.DateTime,
                        IsCanceled = ue.IsCanceled,
                        Genre = new Model.Exhibit.GenreT { Name = ue.Genre.Name },
                        Photographer = new Model.Exhibit.PhotographerT { Name = ue.Photographer.Name }
                    }).ToList().ToPagedList(pageNumber,pageSize); // isToList necessary?

                return model;
            }
        }
    }
}
