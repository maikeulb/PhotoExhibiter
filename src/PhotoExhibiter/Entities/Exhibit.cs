using System;
using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Features.Exhibits;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Entities
{
    public class Exhibit: IEntity
    {
        private readonly List<Attendance> _attendances = new List<Attendance> ();

        public int Id { get; private set; }
        public int GenreId { get; private set; }
        public string PhotographerId { get; private set; }
        public string Location { get; private set; }
        public string ImageUrl { get; private set; }
        public DateTime DateTime { get; private set; }
        public bool IsCanceled { get; private set; }
        public ApplicationUser Photographer { get; private set; }
        public Genre Genre { get; private set; }
        public IEnumerable<Attendance> Attendances => _attendances.AsReadOnly ();

        private Exhibit () { }

        private Exhibit (Create.Command command)
        {
            GenreId = command.GenreId;
            PhotographerId = command.UserId;
            Location = command.Location;
            DateTime = command.DateTime;
            ImageUrl = command.ImageUrl;
        }

        public static Exhibit Create (Create.Command command) => new Exhibit (command);

        public void Cancel ()
        {
            IsCanceled = true;

            var notification = Notification.ExhibitCanceled (this);

            foreach (var attendee in Attendances.Select (a => a.Attendee))
                attendee.Notify (notification);

        }

        public void UpdateDetails (Edit.Command command)
        {
            var notification = Notification.ExhibitUpdated (this, DateTime, Location);

            Id = command.Id;
            Location = command.Location;
            DateTime = command.DateTime;
            GenreId = command.GenreId;

            if (command.ImageUrl != null)
            {
                ImageUrl = command.ImageUrl;
            }

            foreach (var attendee in Attendances.Select (a => a.Attendee))
                attendee.Notify (notification);
        }

        public void ManagerUpdate (string location, DateTime dateTime, string imageUrl)
        {
            var notification = Notification.ExhibitUpdated (this, DateTime, Location);

            Location = location;
            DateTime = dateTime;

            if (imageUrl != null)
            {
                ImageUrl = imageUrl;
            }

            if ((Attendances != null) && (!Attendances.Any()))
            {
                foreach (var attendee in Attendances.Select (a => a.Attendee))
                    attendee.Notify (notification);
            }
        }

        public void AddAttendance (Attendance attendance) => _attendances.Add (attendance);

        public void RemoveAttendance (Attendance attendance) => _attendances.Remove (attendance);
    }
}
