using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Features;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Features.Users
{
    public class Index
    {
        public class Query : IRequest<Model>
        {
            public string UserId { get; set; }
            public string ImageUrl { get; set; }
            public string PhotographerId { get; set; }
            public string PhotographerName { get; set; }
            public bool ShowActions { get; set; }
            public string SearchTerm {get; set;}
        }

        public class Model
        {
            private readonly List<Attendance> _attendances = new List<Attendance> ();

            public string UserId {get; set; }
            public string PhotographerId {get; set; }
            public string PhotographerName {get; set; }
            public string PhotographerEmail {get; set; }
            public string Heading { get; set; }
            public string ImageUrl { get; set; }
            public string SearchTerm { get; set; }
            public bool ShowActions { get; set; }
            public bool IsFollowing { get; set; }
            public IEnumerable<Exhibit> UpcomingExhibits { get; set; }
            public IEnumerable<Exhibit> AttendingExhibits { get; set; }
            public IEnumerable<Attendance> Attendances {get; set;}
            public IEnumerable<ApplicationUser> Followers {get; set; }
            public IEnumerable<ApplicationUser> Following {get; set; }
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
                var photographerEmail = _applicationUserRepository.GetPhotographerEmailById (message.PhotographerId);
                var photographer = _applicationUserRepository.GetUserById (message.PhotographerId);

                var following = _applicationUserRepository.GetPhotographersFollowedBy (message.PhotographerId);
                var followers = _applicationUserRepository.GetPhotographersFollowing (message.PhotographerId);

                var upcomingExhibits = _exhibitRepository.GetUpcomingExhibitsByPhotographer (message.PhotographerId);
                var attendingExhibits = _exhibitRepository.GetExhibitsUserAttending (message.PhotographerId);

                var isFollowing = _followingRepository.GetFollowing(message.UserId, message.PhotographerId) != null;

                var exhibits = new Model
                {
                    PhotographerId = message.PhotographerId,
                    PhotographerName = message.PhotographerName,
                    PhotographerEmail = photographerEmail,
                    UserId = message.UserId,
                    ShowActions = message.ShowActions,
                    ImageUrl = photographer.ImageUrl,
                    UpcomingExhibits = upcomingExhibits,
                    AttendingExhibits = attendingExhibits,
                    Attendances = attendances,
                    Followers = followers,
                    Following = following,
                    IsFollowing = isFollowing,
                    Heading = "[Users] Exhibits"
                };
                return exhibits;
            }
        }
    }
}
