using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PhotoExhibiter.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }
    }
}
