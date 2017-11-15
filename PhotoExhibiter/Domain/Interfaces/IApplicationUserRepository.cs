using System.Collections.Generic;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Domain.Interfaces
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetPhotographersFollowedBy(string userId);
        bool SaveAll();
    }
}