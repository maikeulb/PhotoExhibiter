using System;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Exhibits;
using System.Linq;
using Xunit;

namespace PhotoExhibiter.Tests.Entities.ApplicationUserTests
{
    public class Notify
    {
        private string _testUsername = "test_username";
        private string _testName = "test_name";
        private string _testImageUrl = "test_image_url";

        [Fact]
        public void NotifyUser()
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

            var user = new ApplicationUser 
            {
                Email =_testUsername, 
                Name = _testName, 
                ImageUrl = _testImageUrl
            };

            var notification = Notification.ExhibitCreated(exhibit);
            user.Notify(notification);
            var userNotification = user.UserNotifications.SingleOrDefault();

            Assert.Equal(_testName, userNotification.User.Name);
        }
    }
}
