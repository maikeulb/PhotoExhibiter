using System;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Users;
using System.Linq;
using Xunit;

namespace PhotoExhibiter.Tests.Entities.ApplicationUserTests
{
    public class UpdateDetails
    {
        private string _testUsername = "test_username";
        private string _testName = "test_name";
        private string _testImageUrl = "test_image_url";
        private string _testNewImageUrl = "test_new_image_url";

        [Fact]
        public void UpdateDetailsForUser()
        {
            var user = new ApplicationUser 
            {
                Email =_testUsername, 
                Name = _testName, 
                ImageUrl = _testImageUrl
            };
            var userCommand = new Update.Command
            {
                ImageUrl = _testNewImageUrl
            };
            user.UpdateDetails(userCommand);

            Assert.Equal(_testNewImageUrl, user.ImageUrl);
        }
    }
}
