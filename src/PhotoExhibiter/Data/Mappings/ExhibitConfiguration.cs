using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Data.Mappings
{
    public class ExhibitConfiguration : IEntityTypeConfiguration<Exhibit>
    {
        public void Configure (EntityTypeBuilder<Exhibit> builder)
        {
            builder.Property (e => e.PhotographerId)
                .IsRequired ();

            builder.Property (e => e.GenreId)
                .IsRequired ();

            builder.Property (e => e.DateTime)
                .HasColumnType("Date");

            builder.Property (e => e.Location)
                .IsRequired ()
                .HasMaxLength (255);

            builder.HasMany (e => e.Attendances)
                .WithOne (a => a.Exhibit)
                .OnDelete (DeleteBehavior.Restrict);
        }
    }
}
