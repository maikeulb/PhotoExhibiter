using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoExhibiter.Models
{
    public class Exhibit
    {
        public int Id { get; set; }

        public bool IsCanceled { get; set; }

        public ApplicationUser Photographer { get; set; }

        [Required]
        public string PhotographerId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength (255)]
        public string Location { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public int GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
    }
}