using System.Linq;
using CSharpFunctionalExtensions;
using MediatR;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Features.Apis.Notifications
{
    public class Read
    {
        public class Command : IRequest<Result>
        {
            public string UserId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IUserNotificationRepository _repository;

            public Handler (IUserNotificationRepository repository) => _repository = repository;

            public Result Handle (Command message)
            {
                var notifications = _repository.GetUserNotificationsFor (message.UserId);
                if (notifications != null)
                    return Result.Fail<Command> ("Notification does not exists.");

                notifications.ToList ().ForEach (n => n.Read ()); //event
                _repository.SaveAll ();

                return Result.Ok ();
            }
        }
    }
}
