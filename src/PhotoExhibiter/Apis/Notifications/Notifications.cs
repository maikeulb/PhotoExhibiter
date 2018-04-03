using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediatR;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Apis.Notifications
{
    public class Notifications
    {
        public class Query : IRequest<IEnumerable<Dto>>
        {
            public string UserId { get; set; }
        }

        public class Dto
        {
            public string OriginalLocation { get; set; }
            public DateTime DateTime { get; set; }
            public DateTime? OriginalDateTime { get; set; }
            public NotificationType Type { get; set; }
            public ExhibitDto Exhibit { get; set; }
        }

        public class ExhibitDto
        {
            public int Id { get; set; }
            public string Location { get; set; }
            public DateTime DateTime { get; set; }
            public bool IsCanceled { get; set; }
            public UserDto Photographer { get; set; }
            public GenreDto Genre { get; set; }
        }

        public class GenreDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class UserDto
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Dto>>
        {
            private readonly INotificationRepository _repository;

            public Handler (INotificationRepository repository) => _repository = repository;

            public IEnumerable<Dto> Handle (Query message)
            {
                var notifications = _repository.GetNewNotificationsFor (message.UserId);
                var dTo = notifications.Select (Mapper.Map<Notification, Dto>);

                return dTo;
            }
        }
    }
}
