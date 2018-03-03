using System.Collections.Generic;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Entities.Interfaces
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotificationsFor (string userId);
        bool SaveAll ();
    }
}