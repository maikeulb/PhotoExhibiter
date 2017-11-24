using PhotoExhibiter.Models.Entities;

namespace PhotoExhibiter.Models.Interfaces
{
    public interface IFollowingRepository
    {
        Following GetFollowing (string followerId, string followeeId);
        void Add (Following following);
        void Remove (Following following);
        bool SaveAll ();
    }
}
