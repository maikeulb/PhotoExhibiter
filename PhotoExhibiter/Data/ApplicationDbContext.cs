using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Models;

namespace PhotoExhibiter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public DbSet<Exhibit> Exhibits { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances{ get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>( m =>
            {
                m.HasKey(e => new { e.ExhibitId, e.AttendeeId});

                m.HasOne(e => e.Exhibit)
                    .WithMany(e => e.Attendances)
                    .HasForeignKey(e => e.ExhibitId)
                    .OnDelete(DeleteBehavior.Restrict);

                m.HasOne(e => e.Attendee)
                    .WithMany(e => e.Attendances)
                    .HasForeignKey(e => e.AttendeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ApplicationUser>( m => 
            {
                m.HasMany(e => e.Followers)
                    .WithOne(e => e.Followee)
                    .OnDelete(DeleteBehavior.Restrict);

                m.HasMany(e => e.Followees)
                    .WithOne(e => e.Follower)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Following>()
                .HasKey(e => new { e.FollowerId , e.FolloweeId});

            modelBuilder.Entity<UserNotification>( m =>
            {
                m.HasKey(e => new { e.UserId, e.NotificationId});

                m.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                m.HasOne(e => e.Notification)
                    .WithMany()
                    .HasForeignKey(e => e.NotificationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
