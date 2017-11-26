using System.Collections.Generic;
using PhotoExhibiter.Models.Entities;

namespace PhotoExhibiter.Models.Interfaces
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetPhotographersFollowedBy (string userId);
        bool SaveAll ();
    }
}