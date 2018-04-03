using System.Collections.Generic;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Entities.Interfaces
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetAllPhotographers (string id);
        IEnumerable<ApplicationUser> GetPhotographerFollowers (string id);
        IEnumerable<ApplicationUser> GetPhotographerFollowing (string id);
        ApplicationUser GetPhotographer (string id);
        ApplicationUser GetPhotographerWithExhibits (string id);
        bool SaveAll ();
    }
}
