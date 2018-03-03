using System.Collections.Generic;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Entities.Interfaces
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetPhotographersFollowedBy (string userId);
        bool SaveAll ();
    }
}
