using System.Collections.Generic;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Domain.Interfaces
{
    public interface IExhibitRepository
    {
        Exhibit GetExhibit(int exhibitId);
        IEnumerable<Exhibit> GetUpcomingExhibitsByPhotographer(string photographerId);
        Exhibit GetExhibitWithAttendees(int exhibitId);
        IEnumerable<Exhibit> GetExhibitsUserAttending(string userId);
        void Add(Exhibit exhibit);
        IEnumerable<Exhibit> GetUpcomingExhibits(string searchTerm = null);
        bool SaveAll();
    }
}