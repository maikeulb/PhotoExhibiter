using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PhotoExhibiter.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }

        /* public void Notify(Notification notification) */
        /* { */
        /* UserNotifications.Add(new UserNotification(this, notification)); */
        /* } */
    }
}