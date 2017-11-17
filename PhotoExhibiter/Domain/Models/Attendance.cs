namespace PhotoExhibiter.Domain.Models
{
    public class Attendance
    {
        public int ExhibitId { get; set; }
        public Exhibit Exhibit { get; set; }
        public string AttendeeId { get; set; }
        public ApplicationUser Attendee { get; set; }
    }
}
