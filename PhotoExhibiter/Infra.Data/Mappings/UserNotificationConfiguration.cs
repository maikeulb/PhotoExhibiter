using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Infra.Data.Mappings
{
    public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure (EntityTypeBuilder<UserNotification> builder)
        {
            builder.HasKey (un => new { un.UserId, un.NotificationId });

            builder.HasOne (e => e.User)
                .WithMany (u => u.UserNotifications)
                .HasForeignKey (e => e.UserId)
                .OnDelete (DeleteBehavior.Restrict);

            builder.HasOne (un => un.Notification)
                .WithMany ()
                .HasForeignKey (un => un.NotificationId)
                .OnDelete (DeleteBehavior.Restrict);

            builder.HasOne (un => un.User)
                .WithMany (u => u.UserNotifications)
                .HasForeignKey (un => un.UserId)
                .OnDelete (DeleteBehavior.Restrict);
        }
    }
}
