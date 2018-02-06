using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Infra.Data.Context;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Infra.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository (ApplicationDbContext context) => _context = context;

        public IEnumerable<Genre> GetGenres() => _context.Genres.ToList();

        public bool SaveAll () => _context.SaveChanges () > 0;
    }
}
