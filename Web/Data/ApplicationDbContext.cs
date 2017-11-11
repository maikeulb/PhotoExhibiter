using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .HasKey(a => new { a.GigId, a.AttendeeId});

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Gig)
                .WithMany(g=> g.Attendances)
                .HasForeignKey(a => a.GigId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Attendee)
                .WithMany(au => au.Attendances)
                .HasForeignKey(a => a.AttendeeId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
