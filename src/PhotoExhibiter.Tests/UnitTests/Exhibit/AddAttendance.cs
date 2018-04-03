using System;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Exhibits;
using PhotoExhibiter.Apis.Attendances;
using System.Linq;
using Xunit;

namespace PhotoExhibiter.Tests.Entities.ExhibitTests
{
    public class AddAttendance
    {
        private string _attendeeId = "attendeeId";
        private int _exhibitId = 1;

        [Fact]
        public void IsReadIsTrue()
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

            Assert.Equal(_exhibitId, exhibit.Attendances.SingleOrDefault().ExhibitId);
            Assert.Equal(_attendeeId, exhibit.Attendances.SingleOrDefault().AttendeeId);
        }
    }
}
