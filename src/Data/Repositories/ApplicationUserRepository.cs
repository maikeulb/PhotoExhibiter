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

        public IEnumerable<ApplicationUser> GetAllPhotographers (string searchTerm = null)
        {
            var photographers = _context.Users;;

            return photographers.ToList ();
        }

        public IEnumerable<ApplicationUser> GetPhotographerFollowers (string id)
        {
            return _context.Followings
                .Where (f => f.FollowerId == id)
                .Select (f => f.Followee)
                .ToList ();
        }

        public IEnumerable<ApplicationUser> GetPhotographerFollowing (string id)
        {
            return _context.Followings
                .Where (f => f.FolloweeId == id)
                .Select (f => f.Follower)
                .ToList ();
        }

        public ApplicationUser GetPhotographer (string id)
        {
            return _context.Users
                .Where (u => u.Id == id)
                .SingleOrDefault();
        }
  
        public bool SaveAll () => _context.SaveChanges () > 0;
    }
}
