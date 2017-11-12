using System.Collections.Generic;
using PhotoExhibiter.Models;

namespace PhotoExhibiter.ViewModels
{
    public class ExhibitsViewModel
    {
        public IEnumerable<Exhibit> UpcomingExhibits { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
    }
}
