using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Data.Context;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository (ApplicationDbContext context) => _context = context;

        public IEnumerable<Genre> GetGenres() => _context.Genres.ToList();

        public bool SaveAll () => _context.SaveChanges () > 0;
    }
}
