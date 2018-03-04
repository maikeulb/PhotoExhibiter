using System.Collections.Generic;
using PhotoExhibiter.Entities;

namespace PhotoExhibiter.Entities.Interfaces
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances (string userId);
        Attendance GetAttendance (int exhibitId, string userId);
        IEnumerable<Attendance> GetAllAttendances ();
        bool SaveAll ();
    }
}
