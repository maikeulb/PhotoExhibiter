using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PhotoExhibiter.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        private readonly List<Following> _followers = new List<Following> ();
        private readonly List<Following> _followees = new List<Following> ();
        private readonly List<UserNotification> _userNotifications = new List<UserNotification> ();

        public string Name { get; set; }

        public IEnumerable<Following> Followers => _followers.AsReadOnly ();
        public IEnumerable<Following> Followees => _followees.AsReadOnly ();
        public IEnumerable<UserNotification> UserNotifications => _userNotifications.AsReadOnly ();

        public void Notify (Notification notification) => _userNotifications.Add (UserNotification.Create (this, notification));
    }
}