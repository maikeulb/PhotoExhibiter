using System;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Exhibits;
using System.Linq;
using Xunit;

namespace PhotoExhibiter.Tests.Entities.ExhibitTests
{
    public class ManagerUpdate
    {
        private string _testLocation = "test_location_url";
        private string _testImageUrl = "test_image_url";
        private string _testNewLocation = "test_new_location";
        private string _testNewImageUrl = "test_new_image_url";

        [Fact]
        public void ExhibitUpdated()
        {
            var exhibitCommand = new Create.Command
            {
                GenreId = 1,
                UserId = "id",
                Location = _testLocation,
                DateTime = DateTime.Today,
                ImageUrl =  _testImageUrl
            };
            var exhibit = Exhibit.Create(exhibitCommand);
            exhibit.ManagerUpdate(
                    _testNewLocation, 
                    DateTime.Today, 
                    _testNewImageUrl);

            Assert.Equal(_testNewImageUrl, exhibit.ImageUrl);
        }
    }
}
