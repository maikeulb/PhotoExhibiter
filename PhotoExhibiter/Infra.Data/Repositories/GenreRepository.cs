using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;
using PhotoExhibiter.Infra.Data.Context;

namespace PhotoExhibiter.Infra.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<Genre> GetGenres ()
        {
            return _context.Genres.ToList ();
        }

        public bool SaveAll() => _context.SaveChanges() > 0;
    }
}
