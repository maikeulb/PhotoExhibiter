using System.Collections.Generic;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Domain.Interfaces
{
    public interface IUserNotificationRepository
    {
        IEnumerable<UserNotification> GetUserNotificationsFor(string userId);
    }
}
