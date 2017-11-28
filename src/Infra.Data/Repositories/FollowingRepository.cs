using System.Linq;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;
using PhotoExhibiter.Infra.Data.Context;

namespace PhotoExhibiter.Infra.Data.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context) => _context = context;

        public Following GetFollowing (string followerId, string followeeId)
        {
            return _context.Followings
                .SingleOrDefault (f => f.FolloweeId == followeeId && f.FollowerId == followerId);
        }

        public void Add (Following following)
        {
            _context.Followings.Add (following);
        }

        public void Remove (Following following)
        {
            _context.Followings.Remove (following);
        }

        public bool SaveAll() => _context.SaveChanges() > 0;
    }
}
