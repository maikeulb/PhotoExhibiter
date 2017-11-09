using System;

namespace Web.Models
{
public class Gig
{
    public int Id { get; set; }
    public ApplicationUser Photographer { get; set; }
    public DateTime DateTime { get; set; }
    public string Venue { get; set; }
    public Genre Genre { get; set; }

}
}
