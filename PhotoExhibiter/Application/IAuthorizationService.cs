using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Application
{
    public interface IExhibitService
    {
        Exhibit GetExhibit (int exhibitId);

        bool IsPhotographerExhibitOwner (Exhibit exhibit, string userId);
    }
}