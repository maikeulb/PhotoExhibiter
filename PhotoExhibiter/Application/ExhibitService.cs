using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Application
{
    public class ExhibitService : IExhibitService
    {

        private readonly IExhibitRepository _repository;

        public ExhibitService (IExhibitRepository repository)
        {
            _repository = repository;
        }

        public Exhibit GetExhibit (int exhibitId)
        {
            var exhibit = _repository.GetExhibit (exhibitId);

            return exhibit;
        }

        public bool IsPhotographerExhibitOwner (Exhibit exhibit, string userId)
        {
            if (exhibit.PhotographerId == userId)
                return true;

            return false;
        }
    }
}