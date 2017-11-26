using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;
using PhotoExhibiter.Infra.Data.Context;

namespace PhotoExhibiter.Infra.Data.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserNotificationRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<UserNotification> GetUserNotificationsFor (string userId)
        {
            return _context.UserNotifications
                .Where (un => un.UserId == userId && !un.IsRead)
                .ToList ();
        }

        public bool SaveAll() => _context.SaveChanges() > 0;
    }
}
