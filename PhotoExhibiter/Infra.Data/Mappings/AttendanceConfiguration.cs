using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Infra.Data.Mappings
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure (EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey (a => new { a.ExhibitId, a.AttendeeId });

            builder.HasOne (a => a.Exhibit)
                .WithMany (e => e.Attendances)
                .HasForeignKey (a => a.ExhibitId)
                .OnDelete (DeleteBehavior.Restrict);

            builder.HasOne (a => a.Attendee)
                .WithMany ()
                .HasForeignKey (a => a.AttendeeId)
                .OnDelete (DeleteBehavior.Restrict);
        }
    }
}