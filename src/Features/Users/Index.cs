using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Features;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

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
            public IEnumerable<Attendance> Attendances {get; set;}
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IExhibitRepository _exhibitRepository;
            private readonly IFollowingRepository _followingRepository;
            private readonly IAttendanceRepository _attendanceRepository;

            public Handler(
                    IExhibitRepository exhibitRepository,
                    IFollowingRepository followingRepository,
                    IAttendanceRepository attendanceRepository)
            {
             _exhibitRepository = exhibitRepository;
             _attendanceRepository = attendanceRepository;
             _followingRepository = followingRepository;
            }

            public Model Handle (Query message)
            {
                var upcomingExhibits = _exhibitRepository.GetUpcomingExhibitsByPhotographer (message.PhotographerId);
                var attendances = _attendanceRepository.GetAllAttendances();
                var isFollowing = _followingRepository.GetFollowing(message.UserId, message.PhotographerId) != null;

                var exhibits = new Model
                {
                    PhotographerId = message.PhotographerId,
                    UserId = message.UserId,
                    PhotographerName = message.PhotographerName,
                    UpcomingExhibits = upcomingExhibits,
                    ShowActions = message.ShowActions,
                    Attendances = attendances,
                    IsFollowing = isFollowing,
                    Heading = "[Users] Exhibits"
                };
                return exhibits;
            }
        }
    }
}
