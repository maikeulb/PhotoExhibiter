using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoExhibiter.Models.Entities;

namespace PhotoExhibiter.Data.Mappings
{
    public class FollowingConfiguration : IEntityTypeConfiguration<Following>
    {
        public void Configure (EntityTypeBuilder<Following> builder)
        {
            builder.HasKey (f => new { f.FollowerId, f.FolloweeId });
        }
    }
}
