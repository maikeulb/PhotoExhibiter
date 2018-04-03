using System;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Exhibits;
using System.Linq;
using Xunit;

namespace PhotoExhibiter.Tests.Entities.NotificationTests
{
    public class ExhibitCanceled
    {
        private string _testUsername = "test_username";
        private string _testName = "test_name";
        private string _testImageUrl = "test_image_url";
        private NotificationType _testNotificationType = NotificationType.ExhibitCanceled;

        [Fact]
        public void CancelledExhibitNotification()
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
            var notification = Notification.ExhibitCanceled(exhibit);
            Assert.Equal(_testNotificationType, notification.Type);
        }
    }
}
