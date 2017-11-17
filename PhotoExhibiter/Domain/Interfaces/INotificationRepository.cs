using System.Collections.Generic;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Domain.Interfaces
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotificationsFor(string userId);
        bool SaveAll();
    }
}