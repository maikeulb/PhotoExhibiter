using System;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Exhibits;
using System.Linq;
using Xunit;

namespace PhotoExhibiter.Tests.Entities.UserNotificationTests
{
    public class IsRead
    {
        private string _testUsername = "test_username";
        private string _testName = "test_name";
        private string _testImageUrl = "test_image_url";

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
            var user = new ApplicationUser 
            {
                Email =_testUsername, 
                Name = _testName, 
                ImageUrl = _testImageUrl
            };
            var exhibit = Exhibit.Create(exhibitCommand);
            var notification = Notification.ExhibitCreated(exhibit);
            var userNotification = UserNotification.Create(user, notification);

            /* Assert.Equal(_testCatalogItemId, firstItem.CatalogItemId); */
            /* Assert.Equal(_testUnitPrice, firstItem.UnitPrice); */
            /* Assert.Equal(_testQuantity, firstItem.Quantity); */
        }
    }
}
