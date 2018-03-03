using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Features;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Features.Exhibits
{
    public class Details
    {
        public class Query : IRequest<Model>
        {
            public string UserId { get; set; }
            public int Id { get; set; }
        }

        public class Model
        {
            public Exhibit Exhibit { get; set; }
            public bool IsAttending { get; set; }
            public bool IsFollowing { get; set; }
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IExhibitRepository _exhibitRepository;
            private readonly IAttendanceRepository _attendanceRepository;
            private readonly IFollowingRepository _followingRepository;

            public Handler(
                    IExhibitRepository exhibitRepository,
                    IAttendanceRepository attendanceRepository,
                    IFollowingRepository followingRepository
                    )
            {
             _exhibitRepository = exhibitRepository;
             _attendanceRepository = attendanceRepository;
             _followingRepository = followingRepository;
            }

            public Model Handle (Query message)
            {
                var exhibit = _exhibitRepository.GetExhibit(message.Id);

                var isAttending = _attendanceRepository.GetAttendance(exhibit.Id, message.UserId) != null;

                var isFollowing = _followingRepository.GetFollowing(message.UserId, exhibit.PhotographerId) != null;

                var details = new Model
                {
                    Exhibit = exhibit,
                    IsAttending = isAttending,
                    IsFollowing = isFollowing
                };

                return details;
            }
        }
    }
}
