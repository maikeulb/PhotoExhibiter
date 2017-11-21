using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Infra.Data.Context;

namespace PhotoExhibiter.Infra.Data.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserNotificationRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserNotification> GetUserNotificationsFor (string userId)
        {
            return _context.UserNotifications
                .Where (un => un.UserId == userId && !un.IsRead)
                .ToList ();
        }

        public bool SaveAll ()
        {
            return _context.SaveChanges () > 0;
        }
    }
}
