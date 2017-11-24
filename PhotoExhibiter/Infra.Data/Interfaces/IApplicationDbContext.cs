using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Models.Entities;

namespace PhotoExhibiter.Infra.Data.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<Attendance> Attendances { get; set; }
        DbSet<Exhibit> Exhibits { get; set; }
        DbSet<Following> Followings { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserNotification> UserNotifications { get; set; }
    }
}
