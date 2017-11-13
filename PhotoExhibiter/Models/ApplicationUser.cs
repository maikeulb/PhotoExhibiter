using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PhotoExhibiter.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength (100)]
        public string Name { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

    }
}