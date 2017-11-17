using System.Collections.Generic;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Domain.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
        bool SaveAll();
    }
}