using System.Collections.Generic;
using PhotoExhibiter.Models.Entities;

namespace PhotoExhibiter.Models.Interfaces
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances (string userId);
        Attendance GetAttendance (int exhibitId, string userId);
        /* void Add (Attendance attendance); */
        /* void Remove (Attendance attendance); */
        bool SaveAll ();
    }
}