using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Domain.Entities;
using PhotoExhibiter.Domain.Interfaces;

namespace PhotoExhibiter.Infrastructure.Repositories
{
    public class ExhibitRepository : IExhibitRepository
    {
        private readonly IApplicationDbContext _context;

        public ExhibitRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Exhibit GetExhibit(int exhibitId)
        {
            return _context.Exhibits
                    .Include(g => g.Photographer)
                    .Include(g => g.Genre)
                    .SingleOrDefault(g => g.Id == exhibitId);
        }

        public IEnumerable<Exhibit> GetUpcomingExhibitsByPhotographer(string photographerId)
        {
            return _context.Exhibits
                .Where(g =>
                    g.PhotographerId == photographerId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public Exhibit GetExhibitWithAttendees(int exhibitId)
        {
            return _context.Exhibits
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == exhibitId);
        }

        public IEnumerable<Exhibit> GetExhibitsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Exhibit.DateTime > DateTime.Now)
                .Select(a => a.Exhibit)
                .Include(g => g.Photographer)
                .Include(g => g.Genre)
                .ToList();
        }

        public void Add(Exhibit exhibit)
        {
            _context.Exhibits.Add(exhibit);
        }

        public IEnumerable<Exhibit> GetUpcomingExhibits(string searchTerm = null)
        {
            var upcomingExhibits = _context.Exhibits
                .Include(g => g.Photographer)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!String.IsNullOrWhiteSpace(searchTerm))
            {
                upcomingExhibits = upcomingExhibits
                    .Where(g =>
                            g.Photographer.Name.Contains(searchTerm) ||
                            g.Genre.Name.Contains(searchTerm) ||
                            g.Location.Contains(searchTerm));
            }

            return upcomingExhibits.ToList();
        }
    }
}
