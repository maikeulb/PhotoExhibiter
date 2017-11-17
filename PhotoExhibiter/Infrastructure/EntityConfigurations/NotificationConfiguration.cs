using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Infrastructure.EntityConfigurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure (EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(n => n.Exhibit)
                   .WithMany ()
                   .IsRequired ();
        }
    }
}
