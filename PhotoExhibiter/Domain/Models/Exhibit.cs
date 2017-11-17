using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PhotoExhibiter.Domain.Models
{
    public class Exhibit
    {
        public int Id { get; set; }
        public ApplicationUser Photographer { get; set; }
        public string PhotographerId { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Attendance> Attendances { get; private set; } //Is this ever used?
        public bool IsCanceled { get; private set; }

        public Exhibit()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.ExhibitCanceled(this);

            /* foreach (var attendee in Attendances.Select(a => a.Attendee)) */
            /*     attendee.Notify(notification); */

        }

        public void Modify(DateTime dateTime, string location, int genre)
        {
            /* var notification = Notification.ExhibitUpdated(this, DateTime, Location); */

            Location = location;
            DateTime = dateTime;
            GenreId = genre;

            /* foreach (var attendee in Attendances.Select(a => a.Attendee)) */
            /*     attendee.Notify(notification); */
        }
    }
}
