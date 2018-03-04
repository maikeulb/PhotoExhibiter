using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Features;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;
using X.PagedList;

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
            public int? UpcomingPage { get; set; }
            public int? AttendingPage { get; set; }
            public int? FollowersPage { get; set; }
            public int? FollowingPage { get; set; }
        }

        public class Model
        {
            /* private readonly List<Attendance> _attendances = new List<Attendance> (); */

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

            public IPagedList<Exhibit> UpcomingExhibits { get; set; }
            public IPagedList<Exhibit> AttendingExhibits { get; set; }
            public IPagedList<ApplicationUser> Followers { get; set; }
            public IPagedList<ApplicationUser> Following { get; set; }
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IApplicationUserRepository _applicationUserRepository;
            private readonly IExhibitRepository _exhibitRepository;
            private readonly IFollowingRepository _followingRepository;
            private readonly IAttendanceRepository _attendanceRepository;

            public Handler(
                    IApplicationUserRepository applicationUserrepository,
                    IExhibitRepository exhibitRepository,
                    IFollowingRepository followingRepository,
                    IAttendanceRepository attendanceRepository)
            {
                _applicationUserRepository = applicationUserrepository;
                _exhibitRepository = exhibitRepository;
                _followingRepository = followingRepository;
                _attendanceRepository = attendanceRepository;
            }

            public Model Handle (Query message)
            {
                var attendances = _attendanceRepository.GetAllAttendances();
                var photographer = _applicationUserRepository.GetUserById (message.PhotographerId);

                var following = _applicationUserRepository.GetPhotographersFollowedBy (message.PhotographerId);
                var followers = _applicationUserRepository.GetPhotographersFollowing (message.PhotographerId);

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
                    ImageUrl = photographer.ImageUrl,
                    Attendances = attendances,
                    IsFollowing = isFollowing,
                    Heading = "[Users] Exhibits"
                };

                int pageSize = 4;
                int upcomingPageNumber = (message.UpcomingPage ?? 1);
                int attendingPageNumber = (message.AttendingPage ?? 1);
                int followersPageNumber = (message.FollowersPage ?? 1);
                int followingPageNumber = (message.FollowingPage ?? 1);
                model.UpcomingExhibits = upcomingExhibits.ToPagedList(upcomingPageNumber, pageSize);
                model.AttendingExhibits = attendingExhibits.ToPagedList(attendingPageNumber, pageSize);
                model.Followers = followers.ToPagedList(followersPageNumber, pageSize);
                model.Following = following.ToPagedList(followingPageNumber, pageSize);

                return model;
            }
        }
    }
}
