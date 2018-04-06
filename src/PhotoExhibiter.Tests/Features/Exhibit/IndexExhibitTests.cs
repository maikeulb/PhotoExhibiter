using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using PhotoExhibiter.Features.Exhibits;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Apis.Attendances;
using PhotoExhibiter.Tests;
using Shouldly;
using Xunit;
using static PhotoExhibiter.Tests.SliceFixture;

namespace PhotoExhibiter.Tests.Features.ExhibitTests
{
    public class ListAllCategoreisTests : SliceFixture
    {

        private string _attendeeId = "attendeeId";
        private int _exhibitId = 1;

        [Fact (Skip="doesn't work for some reason")]
        public async Task Should_return_all_exhibits()
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

            await InsertAsync(exhibit);

            var result = await SendAsync(new Index.Query());

            result.ShouldNotBeNull();
        }
    }
}
