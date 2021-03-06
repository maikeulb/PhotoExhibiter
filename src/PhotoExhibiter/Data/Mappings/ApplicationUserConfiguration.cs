using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Data.Mappings
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure (EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property (au => au.Name)
                .IsRequired ();

            builder.Property (au => au.Name)
                .IsRequired ()
                .HasMaxLength (100);

            builder.HasMany (au => au.Exhibits)
                .WithOne (e => e.Photographer)
                .OnDelete (DeleteBehavior.Restrict);

            builder.HasMany (au => au.Followers)
                .WithOne (f => f.Followee)
                .OnDelete (DeleteBehavior.Restrict);

            builder.HasMany (au => au.Followees)
                .WithOne (f => f.Follower)
                .OnDelete (DeleteBehavior.Restrict);
        }
    }
}
