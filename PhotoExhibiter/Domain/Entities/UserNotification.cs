using System;

namespace PhotoExhibiter.Domain.Entities
{
    public class UserNotification
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        public bool IsRead { get; set; }

        public void Read ()
        {
            IsRead = true;
        }
    }
}