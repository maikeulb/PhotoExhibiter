using System;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Exhibits;
using PhotoExhibiter.Apis.Attendances;
using System.Linq;
using Xunit;

namespace PhotoExhibiter.Tests.Entities.ExhibitTests
{
    public class RemoveAttendance
    {
        private string _attendeeId = "attendeeId";
        private int _exhibitId = 1;

        [Fact]
        public void AttendanceRemoved()
        {
            var exhibitCommand = new Create.Command
            {
                GenreId = 1,
                UserId = "id",
                Location = "aperture gallery",
                DateTime = DateTime.Today,
                ImageUrl = "url"
            };
            var exhibit = Exhibit.Create(exhibitCommand);

            var attendanceCommand = new Attend.Command
            {
                UserId = _attendeeId,
                ExhibitId = _exhibitId
            };
            var attendance = Attendance.Create(attendanceCommand);

            exhibit.AddAttendance(attendance);
            exhibit.RemoveAttendance(attendance);

            Assert.Null(exhibit.Attendances.SingleOrDefault());
        }
    }
}
