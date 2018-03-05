using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoExhibiter.Data.Context;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Entities.Interfaces;

namespace PhotoExhibiter.Data.Repositories
{
    public class ExhibitRepository : IExhibitRepository
    {
        private readonly ApplicationDbContext _context;

        public ExhibitRepository (ApplicationDbContext context) => _context = context;

        public IEnumerable<Exhibit> GetAllExhibits (string searchTerm = null)
        {
            var upcomingExhibits = _context.Exhibits;

            return upcomingExhibits.ToList ();
        }

        public IEnumerable<Exhibit> GetUpcomingExhibits (string searchTerm = null)
        {
            var upcomingExhibits = _context.Exhibits
                .Include (g => g.Photographer)
                .Include (g => g.Genre)
                .Where (g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!String.IsNullOrWhiteSpace (searchTerm))
            {
                upcomingExhibits = upcomingExhibits
                    .Where (g =>
                        g.Photographer.Name.Contains (searchTerm) ||
                        g.Genre.Name.Contains (searchTerm) ||
                        g.Location.Contains (searchTerm));
            }

            return upcomingExhibits.ToList ();
        }

        public IEnumerable<Exhibit> GetUpcomingExhibitsByPhotographer (string photographerId)
        {
            return _context.Exhibits
                .Where (g =>
                    g.PhotographerId == photographerId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include (g => g.Genre)
                .ToList ();
        }

        public IEnumerable<Exhibit> GetExhibitsUserAttending (string userId)
        {
            return _context.Attendances
                .Where (a => 
                        a.AttendeeId == userId && 
                        a.Exhibit.DateTime > DateTime.Now)
                .Select (a => a.Exhibit)
                .Include (g => g.Photographer)
                .Include (g => g.Genre)
                .ToList ();
        }

        public Exhibit GetExhibit (int exhibitId)
        {
            return _context.Exhibits
                .Include (e => e.Attendances)
                .Include (e => e.Photographer)
                .Include (e => e.Genre)
                .SingleOrDefault (e => e.Id == exhibitId);
        }

        public Exhibit GetExhibitWithAttendees (int exhibitId)
        {
            return _context.Exhibits
                .Include (e => e.Attendances)
                .ThenInclude (a => a.Attendee)
                .SingleOrDefault (e => e.Id == exhibitId);
        }

        public void Add(Exhibit exhibit) => _context.Exhibits.Add(exhibit);

        public bool SaveAll () => _context.SaveChanges () > 0;
    }
}
