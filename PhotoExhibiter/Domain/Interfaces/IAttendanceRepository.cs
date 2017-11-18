using System.Collections.Generic;
using PhotoExhibiter.Domain.Models;

namespace PhotoExhibiter.Domain.Interfaces
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances (string userId);
        Attendance GetAttendance (int exhibitId, string userId);
        void Add (Attendance attendance);
        void Remove (Attendance attendance);
        bool SaveAll ();
    }
}