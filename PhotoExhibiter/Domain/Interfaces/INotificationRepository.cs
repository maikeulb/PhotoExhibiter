using System.Collections.Generic;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Domain.Interfaces
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotificationsFor(string userId);
    }
}
