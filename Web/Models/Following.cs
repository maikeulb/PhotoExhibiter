using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Following
    {
        [Key]
        public string FollowerId { get; set; }

        [Key]
        public string FolloweeId { get; set; }

        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }
    }
}
