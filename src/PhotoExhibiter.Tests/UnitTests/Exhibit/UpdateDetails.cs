using System;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Exhibits;
using System.Linq;
using Xunit;

namespace PhotoExhibiter.Tests.Entities.ExhibitTests
{
    public class UpdateDetails
    {
        private string _testImageUrl = "test_image_url";
        private string _testLocation = "test_location_url";
        private string _testNewLocation = "test_new_location";
        private string _testNewImageUrl = "test_new_image_url";

        [Fact]
        public void DetailsUpdated()
        {

            var exhibitCommand = new Create.Command
            {
                GenreId = 1,
                UserId = "id",
                Location = _testLocation,
                DateTime = DateTime.Today,
                ImageUrl = _testImageUrl
            };
            var exhibit = Exhibit.Create(exhibitCommand);

            var exhibitEditCommand = new Edit.Command
            {
                Location = _testNewLocation,
                DateTime = DateTime.Today,
                ImageUrl = _testNewImageUrl
            };

            exhibit.UpdateDetails(exhibitEditCommand);

            Assert.Equal(_testNewImageUrl, exhibit.ImageUrl);
        }
    }
}
