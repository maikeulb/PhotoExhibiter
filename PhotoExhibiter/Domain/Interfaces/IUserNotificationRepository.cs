using System.Collections.Generic;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Domain.Interfaces
{
    public interface IUserNotificationRepository
    {
        IEnumerable<UserNotification> GetUserNotificationsFor(string userId);
        bool SaveAll();
    }
}