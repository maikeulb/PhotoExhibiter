using System;
using System.Collections.Generic;
using System.Linq;
using PhotoExhibiter.Infra.Data.Context;
using PhotoExhibiter.Models.Entities;
using PhotoExhibiter.Models.Interfaces;

namespace PhotoExhibiter.Infra.Data.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository (ApplicationDbContext context) => _context = context;

        public IEnumerable<Attendance> GetFutureAttendances (string userId)
        {
            return _context.Attendances
                .Where (a => a.AttendeeId == userId && a.Exhibit.DateTime > DateTime.Now)
                .ToList ();
        }

        public Attendance GetAttendance (int exhibitId, string userId)
        {
            return _context.Attendances
                .SingleOrDefault (a => a.ExhibitId == exhibitId && a.AttendeeId == userId);
        }

        public IEnumerable<Attendance> GetAllAttendances ()
        {
            return _context.Attendances
                .ToList ();
        }

        /* public void Add (Attendance attendance) */
        /* { */
        /*     _context.Attendances.Add (attendance); */
        /* } */

        /* public void Remove (Attendance attendance) */
        /* { */
        /*     _context.Attendances.Remove (attendance); */
        /* } */

        public bool SaveAll () => _context.SaveChanges () > 0;
    }
}
