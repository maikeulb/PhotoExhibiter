using System.Collections.Generic;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Entities.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres ();
        bool SaveAll ();
    }
}
