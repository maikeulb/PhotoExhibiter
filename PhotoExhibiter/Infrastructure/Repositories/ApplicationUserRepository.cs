using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Infrastructure.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetPhotographersFollowedBy (string userId)
        {
            return _context.Followings
                .Where (f => f.FollowerId == userId)
                .Select (f => f.Followee)
                .ToList ();
        }

        public bool SaveAll ()
        {
            return _context.SaveChanges () > 0;
        }
    }
}