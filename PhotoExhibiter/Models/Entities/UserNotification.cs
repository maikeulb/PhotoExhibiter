namespace PhotoExhibiter.Models.Entities
{
    public class UserNotification
    {
        public string UserId { get; private set; }
        public int NotificationId { get; private set; }
        public bool IsRead { get; private set; }
        public Notification Notification { get; private set; }
        public ApplicationUser User { get; private set; }

        private UserNotification () {}

        private UserNotification (string userId, int notificationId)
        {
            UserId = userId;
            NotificationId = notificationId;
        }

        public static UserNotification Create (string userId, int notificationId)
        {
            return new UserNotification (userId, notificationId);
        }

        public void Read ()
        {
            IsRead = true;
        }
    }
}
