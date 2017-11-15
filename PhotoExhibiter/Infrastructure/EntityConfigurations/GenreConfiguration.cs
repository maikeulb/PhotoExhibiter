using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Infrastructure.EntityConfigurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property (g => g.Name)
                   .IsRequired ()
                   .HasMaxLength (255);
        }
    }
}
