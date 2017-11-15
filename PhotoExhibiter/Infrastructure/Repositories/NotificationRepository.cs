using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;

namespace PhotoExhibiter.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public NotificationRepository (IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNewNotificationsFor (string userId)
        {
            return _context.UserNotifications
                .Where (un => un.UserId == userId && !un.IsRead)
                .Select (un => un.Notification)
                .Include (n => n.Exhibit.Photographer)
                .ToList ();
        }
    }
}
