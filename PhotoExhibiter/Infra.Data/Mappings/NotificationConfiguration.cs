using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Infra.Data.Mappings
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure (EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne (n => n.Exhibit)
                .WithMany ()
                .IsRequired ();
        }
    }
}
