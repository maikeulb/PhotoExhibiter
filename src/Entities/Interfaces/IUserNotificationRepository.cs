using System.Collections.Generic;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Entities.Interfaces
{
    public interface IUserNotificationRepository
    {
        IEnumerable<UserNotification> GetUserNotificationsFor (string userId);
        bool SaveAll ();
    }
}