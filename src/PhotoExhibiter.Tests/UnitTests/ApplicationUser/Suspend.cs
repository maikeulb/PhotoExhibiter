using System;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Exhibits;
using System.Linq;
using Xunit;

namespace PhotoExhibiter.Tests.Entities.ApplicationUserTests
{
    public class Suspend
    {
        private string _testUsername = "test_username";
        private string _testName = "test_name";
        private string _testImageUrl = "test_image_url";
        private bool _testIsSuspended = true;

        [Fact]
        public void SuspendUser()
        {
            var exhibitCommand = new Create.Command
            {
                GenreId = 1,
                UserId = "id",
                Location = "aperture gallery",
                DateTime = DateTime.Today,
                ImageUrl = "url"
            };
            var user = new ApplicationUser 
            {
                Email =_testUsername, 
                Name = _testName, 
                ImageUrl = _testImageUrl
            };
            var exhibit = Exhibit.Create(exhibitCommand);
            var notification = Notification.ExhibitCreated(exhibit);
            user.Suspend();

            Assert.Equal(_testIsSuspended, user.IsSuspended);
        }
    }
}
