using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PhotoExhibiter.Features.Exhibits;

namespace PhotoExhibiter.Models.Entities
{
    public class Exhibit
    {
        private readonly List<Attendance> _attendances = new List<Attendance>();

        public int Id { get; private set; }
        public int GenreId { get; private set; }
        public string PhotographerId { get; private set; }
        public string Location { get; private set; }
        public DateTime DateTime { get; private set; }
        public bool IsCanceled { get; private set; }
        public ApplicationUser Photographer { get; private set; }
        public Genre Genre { get; private set; }

        public IEnumerable<Attendance> Attendances => _attendances.AsReadOnly();

        private Exhibit () {}

        private Exhibit (Create.Command command)
        {
            GenreId = command.GenreId;
            PhotographerId = command.UserId;
            Location = command.Location;
            DateTime = command.DateTime;
        }

        public static Exhibit Create(Create.Command command) => new Exhibit(command);

        public void Cancel ()
        {
            IsCanceled = true; //alternatively, create the notification object and call the method.

            Notification.ExhibitCanceled (this);
        }

        public void UpdateDetails (Edit.Command command)
        {
            Location = command.Location;
            DateTime = command.DateTime;
            GenreId = command.GenreId;
        }

        public void AddAttendance (Attendance attendance)
        {
            _attendances.Add (attendance);
        }

        public void RemoveAttendance (Attendance attendance)
        {
            _attendances.Remove (attendance);
        }
    }
}
