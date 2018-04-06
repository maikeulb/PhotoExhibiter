using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PhotoExhibiter.Data.Interfaces;
using PhotoExhibiter.Data.Mappings;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private IDbContextTransaction _currentTransaction;

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

            builder.Entity<Exhibit> ()
                .Metadata.FindNavigation (nameof (Exhibit.Attendances))
                .SetPropertyAccessMode (PropertyAccessMode.Field);

            builder.Entity<ApplicationUser> ()
                .Metadata.FindNavigation (nameof (ApplicationUser.Followers))
                .SetPropertyAccessMode (PropertyAccessMode.Field);

            builder.Entity<ApplicationUser> ()
                .Metadata.FindNavigation (nameof (ApplicationUser.Followees))
                .SetPropertyAccessMode (PropertyAccessMode.Field);

            builder.Entity<ApplicationUser> ()
                .Metadata.FindNavigation (nameof (ApplicationUser.Exhibits))
                .SetPropertyAccessMode (PropertyAccessMode.Field);

            base.OnModelCreating (builder);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
