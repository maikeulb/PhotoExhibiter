using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Models;

namespace PhotoExhibiter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public DbSet<Exhibit> Exhibits { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances{ get; set; }
        public DbSet<Following> Followings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasKey(a => new { a.ExhibitId, a.AttendeeId});

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Exhibit)
                .WithMany(g=> g.Attendances)
                .HasForeignKey(a => a.ExhibitId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Attendee)
                .WithMany(au => au.Attendances)
                .HasForeignKey(a => a.AttendeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.Followers)
                .WithOne(f => f.Followee)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(au => au.Followees)
                .WithOne(f => f.Follower)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Following>()
                .HasKey(f => new { f.FollowerId , f.FolloweeId});

                base.OnModelCreating(modelBuilder);
        }
    }
}
