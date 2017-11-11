
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Attendance
    {
        [Key]
        public int GigId { get; set; }
        public Gig Gig { get; set; }

        [Key]
        public string AttendeeId { get; set; }
        public ApplicationUser Attendee { get; set; }

    }
}
