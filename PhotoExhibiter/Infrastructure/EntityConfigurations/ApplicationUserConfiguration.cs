using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Infrastructure.EntityConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property (au => au.Name)
                   .IsRequired ()
                   .HasMaxLength (100);

            builder.HasMany (au => au.Followers)
                   .WithOne (f => f.Followee)
                   .OnDelete (DeleteBehavior.Restrict);

            builder.HasMany (au => au.Followees)
                   .WithOne (f => f.Follower)
                   .OnDelete (DeleteBehavior.Restrict);
        }
    }
}
