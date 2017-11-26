namespace PhotoExhibiter.Models.Entities
{
    public class UserNotification
    {
        public string UserId { get; private set; }
        public int NotificationId { get; private set; }
        public bool IsRead { get; private set; }
        public Notification Notification { get; private set; }
        public ApplicationUser User { get; private set; }

        private UserNotification () { }

        private UserNotification (ApplicationUser user, Notification notification)
        {
            UserId = user.Id;
            NotificationId = notification.Id;
            Notification = notification;
            User = user;
        }

        public static UserNotification Create (ApplicationUser user, Notification notification)
        {
            return new UserNotification (user, notification);
        }

        public void Read () => IsRead = true;
    }
}