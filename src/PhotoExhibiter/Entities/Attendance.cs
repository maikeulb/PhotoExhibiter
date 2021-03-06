﻿using PhotoExhibiter.Apis.Attendances;

namespace PhotoExhibiter.Entities
{
    public class Attendance
    {
        public int ExhibitId { get; private set; }
        public string AttendeeId { get; private set; }
        public Exhibit Exhibit { get; private set; }
        public ApplicationUser Attendee { get; private set; }

        private Attendance () { }

        private Attendance (Attend.Command command)
        {
            ExhibitId = command.ExhibitId;
            AttendeeId = command.UserId;
        }

        public static Attendance Create (Attend.Command command) => new Attendance (command);
    }
}
