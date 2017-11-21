using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PhotoExhibiter.Domain.Entities
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
        public bool IsCanceled { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public void Cancel ()
        {
            IsCanceled = true;

            Notification.ExhibitCanceled (this);
        }

        public void UpdateDetails (DateTime dateTime, string location, int genreId)
        {
            Location = location;
            DateTime = dateTime;
            GenreId = genreId;
        }
    }
}