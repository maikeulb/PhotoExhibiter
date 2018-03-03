using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Entities.Interfaces
{
    public interface IFollowingRepository
    {
        Following GetFollowing (string followerId, string followeeId);
        void Add (Following following);
        void Remove (Following following);
        bool SaveAll ();
    }
}