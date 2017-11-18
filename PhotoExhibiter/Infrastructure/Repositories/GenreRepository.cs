using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Domain.Interfaces;
using PhotoExhibiter.Domain.Models;

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

        public bool SaveAll ()
        {
            return _context.SaveChanges () > 0;
        }
    }
}