
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Attendance
    {
        [Key]
        public int GigId { get; set; }
        public Gig Gig { get; set; }

        [Key]
        public int AttendeeId { get; set; }
        public ApplicationUser Attendee { get; set; }

    }
}
