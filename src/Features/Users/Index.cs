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
            public string Heading { get; set; }
            public string SearchTerm { get; set; }
            public bool ShowActions { get; set; }
            public bool IsFollowing { get; set; }
            public IEnumerable<Exhibit> UpcomingExhibits { get; set; }
            public IEnumerable<Exhibit> MyUpcomingExhibits { get; set; }
            public IEnumerable<Attendance> Attendances {get; set;}
            public IEnumerable<ApplicationUser> Followers {get; set; }
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
                var myUpcomingExhibits = _exhibitRepository.GetExhibitsUserAttending (message.UserId);
                var upcomingExhibits = _exhibitRepository.GetUpcomingExhibitsByPhotographer (message.PhotographerId);
                var attendances = _attendanceRepository.GetAllAttendances();

              // query.PhotographerId = query.UserId; when photographerId ==
              // null. This happens when it is the current user. THERFORE, for
              // the current user, photoID == userID. There is always a userID
                var followers = _applicationUserRepository.GetPhotographersFollowedBy (message.PhotographerId);
                /* if (message.UserId == message.PhotographerId) */ 
                /* { */
                /* } */
                /* else */ 
                /* { */
                    /* var followers = _applicationUserRepository.GetPhotographersFollowedBy (message.PhotographerId); */
                /* } */
                var isFollowing = _followingRepository.GetFollowing(message.UserId, message.PhotographerId) != null;

                var exhibits = new Model
                {
                    PhotographerId = message.PhotographerId,
                    UserId = message.UserId,
                    PhotographerName = message.PhotographerName,
                    ShowActions = message.ShowActions,
                    MyUpcomingExhibits = myUpcomingExhibits,
                    UpcomingExhibits = upcomingExhibits,
                    Attendances = attendances,
                    Followers = followers,
                    IsFollowing = isFollowing,
                    Heading = "[Users] Exhibits"
                };
                return exhibits;
            }
        }
    }
}
