using System;
using System.Linq;
using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Features;
using PhotoExhibiter.Entities.Interfaces;
using PhotoExhibiter.Infrastructure.Interfaces;

namespace PhotoExhibiter.Features.Users
{
    public class Index
    {
        public class Query : IRequest<Model>
        {
            public string UserId { get; set; }
            public string PhotographerId { get; set; }
            public string SearchTerm {get; set;}
            public bool ShowActions { get; set; }
        }

        public class Model
        {
            public string UserId {get; set; }
            public string PhotographerId {get; set; }
            public string PhotographerName {get; set; }
            public string PhotographerEmail {get; set; }
            public string Heading { get; set; }
            public string ImageUrl { get; set; }
            public string SearchTerm { get; set; }
            public bool ShowActions { get; set; }
            public bool IsFollowing { get; set; }
            public IEnumerable<Attendance> Attendances {get; set;}
            public List<Exhibit> UpcomingExhibits { get; set; }
            public List<Exhibit> AttendingExhibits { get; set; }
            public List<PhotographerT> Followers { get; set; }
            public List<PhotographerT> Following { get; set; }

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
                public class GenreT
                {
                    public string Name{ get; set; }
                }
            }

            public class PhotographerT
            {
                public string Id { get; set; }
                public string Name { get; set; }
                public string ImageUrl { get; set; }
                public string Email { get; set; }
            }

            public class Attendance
            {
                public int ExhibitId { get; set; }
                public string AttendeeId { get; set; }
            }
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IApplicationUserRepository _applicationUserRepository;
            private readonly IExhibitRepository _exhibitRepository;
            private readonly IFollowingRepository _followingRepository;
            private readonly IAttendanceRepository _attendanceRepository;
            private readonly IUrlComposer _urlComposer;

            public Handler(IApplicationUserRepository applicationUserrepository,
                    IExhibitRepository exhibitRepository,
                    IFollowingRepository followingRepository,
                    IAttendanceRepository attendanceRepository,
                    IUrlComposer urlComposer)
            {
                _applicationUserRepository = applicationUserrepository;
                _exhibitRepository = exhibitRepository;
                _followingRepository = followingRepository;
                _attendanceRepository = attendanceRepository;
                _urlComposer = urlComposer;
            }

            public Model Handle (Query message)
            {
                var attendances = _attendanceRepository.GetAllAttendances();
                var photographer = _applicationUserRepository.GetPhotographer (message.PhotographerId);

                var following = _applicationUserRepository.GetPhotographerFollowers (message.PhotographerId);
                var followers = _applicationUserRepository.GetPhotographerFollowing (message.PhotographerId);

                var upcomingExhibits = _exhibitRepository.GetUpcomingExhibitsByPhotographer (message.PhotographerId);
                var attendingExhibits = _exhibitRepository.GetExhibitsUserAttending (message.PhotographerId);

                var isFollowing = _followingRepository.GetFollowing(message.UserId, message.PhotographerId) != null;
                
                var model = new Model
                {
                    PhotographerId = message.PhotographerId,
                    PhotographerName = photographer.Name,
                    PhotographerEmail = photographer.Email,
                    UserId = message.UserId,
                    ShowActions = message.ShowActions,
                    Attendances = attendances.Select (a => new Model.Attendance
                    {
                        ExhibitId = a.ExhibitId,
                        AttendeeId = a.AttendeeId
                    }).ToList(),
                    IsFollowing = isFollowing,
                    Heading = "[Users] Exhibits"
                };

                if (photographer.ImageUrl != null) 
                    model.ImageUrl = _urlComposer.ComposeImgUrl(photographer.ImageUrl);

                model.UpcomingExhibits = upcomingExhibits.Select (ue => new Model.Exhibit
                    {
                        Id = ue.Id,
                        PhotographerId = ue.PhotographerId,
                        Location = ue.Location,
                        ImageUrl = _urlComposer.ComposeImgUrl(ue.ImageUrl),
                        DateTime = ue.DateTime,
                        IsCanceled = ue.IsCanceled,
                        Genre = new Model.Exhibit.GenreT { Name = ue.Genre.Name },
                        Photographer = new Model.PhotographerT { Name = ue.Photographer.Name }
                    }).ToList();

                model.AttendingExhibits = attendingExhibits.Select (ae => new Model.Exhibit
                    {
                        Id = ae.Id,
                        PhotographerId = ae.PhotographerId,
                        Location = ae.Location,
                        ImageUrl = _urlComposer.ComposeImgUrl(ae.ImageUrl),
                        DateTime = ae.DateTime,
                        IsCanceled = ae.IsCanceled,
                        Genre = new Model.Exhibit.GenreT { Name = ae.Genre.Name },
                        Photographer = new Model.PhotographerT { Name = ae.Photographer.Name }
                    }).ToList();

                model.Followers = followers.Select (f => new Model.PhotographerT
                    {
                        Id = f.Id,
                        Name = f.Name,
                        ImageUrl = _urlComposer.ComposeImgUrl(f.ImageUrl),
                        Email = f.Email,
                    }).ToList();

                model.Following = following.Select (f => new Model.PhotographerT
                    {
                        Id = f.Id,
                        Name = f.Name,
                        ImageUrl = _urlComposer.ComposeImgUrl(f.ImageUrl),
                        Email = f.Email,
                    }).ToList();

                return model;
            }
        }
    }
}
