using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Infra.Data.Context;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Infra.Data.Repositories
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

        public bool SaveAll () => _context.SaveChanges () > 0;
    }
}