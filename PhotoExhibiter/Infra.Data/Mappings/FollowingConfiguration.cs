using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Infra.Data.Mappings
{
    public class FollowingConfiguration : IEntityTypeConfiguration<Following>
    {
        public void Configure (EntityTypeBuilder<Following> builder)
        {
            builder.HasKey (f => new { f.FollowerId, f.FolloweeId });
        }
    }
}
