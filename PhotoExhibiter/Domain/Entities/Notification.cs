using System;
using PhotoExhibiter.Domain.Enums;

namespace PhotoExhibiter.Domain.Entities
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalLocation { get; private set; }
        public Exhibit Exhibit { get; private set; }

        protected Notification ()
        { }

        private Notification (NotificationType type, Exhibit exhibit)
        {
            if (exhibit == null)
                throw new ArgumentNullException ("exhibit");

            Type = type;
            Exhibit = exhibit;
            DateTime = DateTime.Now;
        }

        public static Notification ExhibitCreated (Exhibit exhibit)
        {
            return new Notification (NotificationType.ExhibitCreated, exhibit);
        }

        public static Notification ExhibitUpdated (Exhibit newExhibit, DateTime originalDateTime, string originalLocation)
        {
            var notification = new Notification (NotificationType.ExhibitUpdated, newExhibit);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalLocation = originalLocation;

            return notification;
        }

        public static Notification ExhibitCanceled (Exhibit exhibit)
        {
            return new Notification (NotificationType.ExhibitCanceled, exhibit);
        }
    }
}
