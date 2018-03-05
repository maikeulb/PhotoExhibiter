using System.Collections.Generic;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Entities.Interfaces
{
    public interface IExhibitRepository
    {
        IEnumerable<Exhibit> GetAllExhibits (string searchTerm = null);
        IEnumerable<Exhibit> GetUpcomingExhibits (string searchTerm = null);
        IEnumerable<Exhibit> GetUpcomingExhibitsByPhotographer (string photographerId);
        IEnumerable<Exhibit> GetExhibitsUserAttending (string userId);
        Exhibit GetExhibit (int exhibitId);
        Exhibit GetExhibitWithAttendees (int exhibitId);
        void Add (Exhibit exhibit);
        bool SaveAll ();
    }
}
