using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PhotoExhibiter.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        private readonly List<Following> _followers = new List<Following> ();
        private readonly List<Following> _followees = new List<Following> ();
        private readonly List<UserNotification> _userNotifications = new List<UserNotification> ();

        public IEnumerable<Following> Followers => _followers.AsReadOnly ();
        public IEnumerable<Following> Followees => _followees.AsReadOnly ();
        public IEnumerable<UserNotification> UserNotifications => _userNotifications.AsReadOnly ();

        public ApplicationUser() : base() {} 

        public ApplicationUser(string userName, string name) : base(userName)
        {
           base.Email = userName;
           Name = name;
        }

        public void Notify (Notification notification) => _userNotifications.Add (UserNotification.Create (this, notification));
    }
}
