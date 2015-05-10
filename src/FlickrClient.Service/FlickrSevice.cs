using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlickrClient.Service
{
    public class FlickrSevice
    {
        private readonly string _apiKey;
        private string rootUrl = "https://api.flickr.com/services/rest";
        private const string MostPopularMethodPlaces = "?method=flickr.places.getTopPlacesList&place_type_id=7&format=json&nojsoncallback=1&api_key={0}";

        public FlickrSevice(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<PlaceDto[]> GetMostPopularPlaces()
        {
            using (var client = new HttpClient())
            {
                var uri = string.Format(rootUrl + "/" + MostPopularMethodPlaces, _apiKey);
                var placesRaw = await client.GetStringAsync(uri);

                var dynamicResult = JsonConvert.DeserializeObject<TopPlacesResponce>(placesRaw);

                return dynamicResult.places.place.Select(rawPlace => new PlaceDto()
                {
                    Id = rawPlace.place_id,
                    Name = rawPlace._content,
                    PhotoCount = rawPlace.photo_count
                }).ToArray();
            }
        }

        private class TopPlacesResponce
        {
            public PlacesResponce places { get; set; }
            public string stat { get; set; }
        }

        private class PlacesResponce
        {
            public int total { get; set; }
            public PlaceResponce[] place { get; set; }
            public string date_start { get; set; }
            public string date_stop { get; set; }
        }

        private class PlaceResponce
        {
            public string place_id { get; set; }
            public string worid { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string place_url { get; set; }
            public string place_type { get; set; }
            public int place_type_id { get; set; }
            public string timezone { get; set; }
            public string _content { get; set; }
            public string woe_name { get; set; }
            public int photo_count { get; set; }
        }
    }
}