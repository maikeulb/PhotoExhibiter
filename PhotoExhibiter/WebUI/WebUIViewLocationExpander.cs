using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace PhotoExhibiter.WebUI
{
    public class WebUIViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations (ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            viewLocations = viewLocations.Select (s => s.Replace ("Views", "WebUI/Views"));

            return viewLocations;
        }

        public void PopulateValues (ViewLocationExpanderContext context) { }
    }
}
