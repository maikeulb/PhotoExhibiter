using System.Collections.Generic;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.WebUI.ViewModels
{
    public class ExhibitsViewModel
    {
        public IEnumerable<Exhibit> UpcomingExhibits { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
    }
}