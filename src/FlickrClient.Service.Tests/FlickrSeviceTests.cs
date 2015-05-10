using FluentAssertions;
using NUnit.Framework;

namespace FlickrClient.Service.Tests
{
    [TestFixture]
    public class FlickrSeviceTests
    {
        private FlickrSevice _service;

        [SetUp]
        public void SetUp()
        {
            _service = new FlickrSevice("3e74c14ad73d3c26c8ecb5dd1451a61a");
        }

        [Test]
        public async void should_load_most_popular_places()
        {
            var places = await _service.GetMostPopularPlaces();

            places.Should().NotBeEmpty();
        }
    }
}
