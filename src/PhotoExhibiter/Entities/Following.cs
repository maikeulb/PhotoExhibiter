using PhotoExhibiter.Apis.Followings;

namespace PhotoExhibiter.Entities
{
    public class Following
    {
        public string FollowerId { get; private set; }
        public string FolloweeId { get; private set; }
        public ApplicationUser Follower { get; private set; }
        public ApplicationUser Followee { get; private set; }

        private Following () { }

        private Following (Follow.Command command)
        {
            FollowerId = command.UserId;
            FolloweeId = command.FolloweeId;
        }

        public static Following Create (Follow.Command command) => new Following (command);
    }
}
