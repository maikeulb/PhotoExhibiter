using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Infra.Data.Interfaces;
using PhotoExhibiter.Infra.Data.Mappings;


namespace PhotoExhibiter.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Exhibit> Exhibits { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            builder.ApplyConfiguration (new ApplicationUserConfiguration ());
            builder.ApplyConfiguration (new AttendanceConfiguration ());
            builder.ApplyConfiguration (new ExhibitConfiguration ());
            builder.ApplyConfiguration (new GenreConfiguration ());
            builder.ApplyConfiguration (new FollowingConfiguration ());
            builder.ApplyConfiguration (new NotificationConfiguration ());
            builder.ApplyConfiguration (new UserNotificationConfiguration ());

            builder.Entity<Exhibit>()
                .Metadata.FindNavigation(nameof(Exhibit.Attendances))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Entity<ApplicationUser>()
                .Metadata.FindNavigation(nameof(ApplicationUser.Followers))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Entity<ApplicationUser>()
                .Metadata.FindNavigation(nameof(ApplicationUser.Followees))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            base.OnModelCreating (builder);
        }
    }
}
