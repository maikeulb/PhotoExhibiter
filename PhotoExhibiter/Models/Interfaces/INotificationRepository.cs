using System.Collections.Generic;
using PhotoExhibiter.Models.Entities;

namespace PhotoExhibiter.Models.Interfaces
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotificationsFor (string userId);
        bool SaveAll ();
    }
}