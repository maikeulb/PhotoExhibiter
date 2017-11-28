using System.Collections.Generic;
using PhotoExhibiter.Models.Entities;

namespace PhotoExhibiter.Models.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres ();
        bool SaveAll ();
    }
}