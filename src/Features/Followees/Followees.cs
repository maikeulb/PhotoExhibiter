using System.Collections.Generic;
using MediatR;
using PhotoExhibiter.Features;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Features.Followees
{
    public class Followees
    {
        public class Query : IRequest<Model>
        {
            public string UserId { get; set; }
            public bool ShowActions { get; set; }
            public string SearchTerm {get; set;}
        }

        public class Model
        {
            private readonly List<Attendance> _attendances = new List<Attendance> ();

            public IEnumerable<Exhibit> UpcomingExhibits { get; set; }
            public string Heading { get; set; }
            public string SearchTerm { get; set; }
            public IEnumerable<ApplicationUser> Photographers {get; set; }

            public IEnumerable<Attendance> Attendances => _attendances.AsReadOnly ();
        }

        public class Handler : IRequestHandler<Query, Model>
        {
            private readonly IApplicationUserRepository _repository;
            private readonly IExhibitRepository _exhibitRepository;

            public Handler (
              IApplicationUserRepository repository,
              IExhibitRepository exhibitRepository) 
            {
                _repository = repository;
                _exhibitRepository = exhibitRepository;
            }

            public Model Handle (Query message)
            {
                var upcomingExhibits = _exhibitRepository.GetUpcomingExhibitsByPhotographer (message.UserId);
                var photographers = _repository.GetPhotographersFollowedBy (message.UserId);
                var model = new Model
                {
                    UpcomingExhibits = upcomingExhibits,
                    Heading = "My Followings",
                    Photographers = photographers
                };

                return model;
            }
        }
    }
}
