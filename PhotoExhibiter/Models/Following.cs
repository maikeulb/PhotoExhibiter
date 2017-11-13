using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Models
{
    public class Following
    {
        [Key]
        public string FollowerId { get; set; }
        public ApplicationUser Follower { get; set; }

        [Key]
        public string FolloweeId { get; set; }
        public ApplicationUser Followee { get; set; }
    }
}