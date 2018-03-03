using System.Collections.Generic;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Entities.Interfaces
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetPhotographersFollowedBy (string userId);
        IEnumerable<ApplicationUser> GetPhotographersFollowing (string userId);
        string GetPhotographerEmailById (string userId);
        ApplicationUser GetUserById (string userId);
        bool SaveAll ();
    }
}
