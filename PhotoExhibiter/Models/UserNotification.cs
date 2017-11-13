using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Models
{
    public class UserNotification
    {
        [Key]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Key]
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }

        public bool IsRead { get; set; }
    }
}