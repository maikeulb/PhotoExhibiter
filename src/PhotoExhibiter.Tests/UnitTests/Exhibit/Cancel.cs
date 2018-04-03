using System;
using PhotoExhibiter.Entities;
using PhotoExhibiter.Features.Exhibits;
using System.Linq;
using Xunit;

namespace PhotoExhibiter.Tests.Entities.ExhibitTests
{
    public class Cancel
    {
        private string _testImageUrl = "test_image_url";
        private bool _testCanceled = true;

        [Fact]
        public void ExhibitCanceled()
        {
            var exhibitCommand = new Create.Command
            {
                GenreId = 1,
                UserId = "id",
                Location = "aperture gallery",
                DateTime = DateTime.Today,
                ImageUrl = _testImageUrl
            };
            var exhibit = Exhibit.Create(exhibitCommand);
            exhibit.Cancel();

            Assert.Equal(_testCanceled, exhibit.IsCanceled);
        }
    }
}
