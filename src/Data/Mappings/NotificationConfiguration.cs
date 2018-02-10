using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoExhibiter.Models.Entities;

namespace PhotoExhibiter.Data.Mappings
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
