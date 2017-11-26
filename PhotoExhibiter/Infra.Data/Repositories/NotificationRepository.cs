using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;
using PhotoExhibiter.Infra.Data.Context;

namespace PhotoExhibiter.Infra.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<Notification> GetNewNotificationsFor (string userId)
        {
            return _context.UserNotifications
                .Where (un => un.UserId == userId && !un.IsRead)
                .Select (un => un.Notification)
                .Include (n => n.Exhibit.Photographer)
                .ToList ();
        }

        public bool SaveAll() => _context.SaveChanges() > 0;
    }
}
