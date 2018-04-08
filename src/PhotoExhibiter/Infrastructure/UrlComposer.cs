using PhotoExhibiter.Infrastructure.Interfaces;

namespace PhotoExhibiter.Infrastructure
{
    public class UrlComposer : IUrlComposer
    {
        private readonly ExhibitSettings _exhibitSettings;

        public UrlComposer(ExhibitSettings exhibitSettings) => _exhibitSettings = exhibitSettings;

        public string ComposeImgUrl(string urlTemplate)
        {
            return urlTemplate.Replace("http://exhibitbaseurl", _exhibitSettings.ExhibitBaseUrl);
        }
    }
}
