using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
public class Gig
{
    public int Id { get; set; }
    [Required]
    public ApplicationUser Photographer { get; set; }
    public DateTime DateTime { get; set; }

    [Required]
    [StringLength(255)]
    public string Venue { get; set; }
    [Required]
    public Genre Genre { get; set; }

}
}
