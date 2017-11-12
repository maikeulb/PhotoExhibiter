
using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Models
{
    public class Attendance
    {
        [Key]
        public int ExhibitId { get; set; }
        public Exhibit Exhibit { get; set; }

        [Key]
        public string AttendeeId { get; set; }
        public ApplicationUser Attendee { get; set; }

    }
}
