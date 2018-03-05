using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using PhotoExhibiter.Features.Users;

namespace PhotoExhibiter.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSuspended { get; set; }

        private readonly List<Following> _followers = new List<Following> ();
        private readonly List<Following> _followees = new List<Following> ();
        private readonly List<UserNotification> _userNotifications = new List<UserNotification> ();

        public IEnumerable<Following> Followers => _followers.AsReadOnly ();
        public IEnumerable<Following> Followees => _followees.AsReadOnly ();
        public IEnumerable<UserNotification> UserNotifications => _userNotifications.AsReadOnly ();

        public ApplicationUser() : base() {} 

        public ApplicationUser(string userName, string name, string imageUrl) : base(userName)
        {
           base.Email = userName;
           Name = name;
           ImageUrl = imageUrl;
        }

        public void UpdateDetails (Update.Command command)
        {
            ImageUrl = command.ImageUrl;
        }

        public void Notify (Notification notification) => _userNotifications.Add (UserNotification.Create (this, notification));

        public void Cancel ()
        {
            IsSuspended = true;

            /* foreach (var exhibit in Exhibits) */
            /* { */
                /* exhibit.Cancel (); */
            /* } */
        }
    }
}
