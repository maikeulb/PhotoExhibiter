using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;

namespace PhotoExhibiter.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres ()
        {
            return _context.Genres.ToList ();
        }

        public bool SaveAll()
        {
          return _context.SaveChanges() > 0;
        }
    }
}
