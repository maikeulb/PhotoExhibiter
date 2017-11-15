using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Exhibit> Exhibits { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Attendance> Attendances { get; set; }
        DbSet<Following> Followings { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
        DbSet<ApplicationUser> Users { get; set; }
    }
}
