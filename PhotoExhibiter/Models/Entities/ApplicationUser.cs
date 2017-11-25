using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PhotoExhibiter.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public ICollection<Following> Followers { get; private set; }
        public ICollection<Following> Followees { get; private set; }
        public ICollection<UserNotification> UserNotifications { get; private set; }
    }
}
