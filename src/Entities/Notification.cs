using System;

namespace PhotoExhibiter.Entities
{
    public class Notification 
    {
        public int Id { get; private set; }
        public string OriginalLocation { get; private set; }
        public DateTime DateTime { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public Exhibit Exhibit { get; private set; }

        protected Notification () { }

        private Notification (NotificationType type, Exhibit exhibit)
        {
            Type = type;
            Exhibit = exhibit;
            DateTime = DateTime.Now;
        }

        private Notification (NotificationType type, Exhibit exhibit, DateTime originalDateTime, string originalLocation)
        {
            Type = type;
            Exhibit = exhibit;
            OriginalDateTime = originalDateTime;
            OriginalLocation = originalLocation;
            DateTime = DateTime.Now;
        }

        public static Notification ExhibitCreated (Exhibit exhibit)
        {
            return new Notification (NotificationType.ExhibitCreated, exhibit);
        }

        public static Notification ExhibitUpdated (Exhibit newExhibit, DateTime originalDateTime, string originalLocation)
        {
            return new Notification (NotificationType.ExhibitUpdated, newExhibit, originalDateTime, originalLocation);
        }

        public static Notification ExhibitCanceled (Exhibit exhibit)
        {
            return new Notification (NotificationType.ExhibitCanceled, exhibit);
        }
    }
}
