using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Data.Context;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Data.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository (ApplicationDbContext context) => _context = context;

        public IEnumerable<ApplicationUser> GetPhotographersFollowedBy (string userId)
        {
            return _context.Followings
                .Where (f => f.FollowerId == userId)
                .Select (f => f.Followee)
                .ToList ();
        }

        public string GetPhotographerEmailById (string userId)
        {
            return _context.Users
                .Where (u => u.Id == userId)
                .Select (u => u.Email)
                .SingleOrDefault();
        }

        public ApplicationUser GetUserById (string userId)
        {
            return _context.Users
                .Where (u => u.Id == userId)
                .SingleOrDefault();
        }
  
        public bool SaveAll () => _context.SaveChanges () > 0;
    }
}
