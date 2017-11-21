using System.Collections.Generic;
using PhotoExhibiter.Domain.Entities;

namespace PhotoExhibiter.Domain.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres ();
        bool SaveAll ();
    }
}