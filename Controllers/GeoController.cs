using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestIPGeo.models;
using Serilog;


namespace RestIPGeo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly HttpClient _httpClient = new();
        private readonly string token = "7fdb012f6c4fa5ca733d6aaea764f3ed6d0a48d3";
        //static readonly string secret = "697191e5c4f062e04247eb3a4af37ee51beea2d5";
        private readonly string BaseUrl = "https://suggestions.dadata.ru/suggestions/api/4_1/rs/geolocate/address";


        [Route("/geo-search")]
        [HttpGet]
        public async Task<string> SearchCoordinates(string country = "RU", string city = "Москва",
            string street = "Красная", int limit = 2)
        {

            Log.Verbose($"get request SearchCoordinates( " +
                            $"country='{country}',  city='{city}',  street='{street}',  limit={limit} )");

            string url = $"https://nominatim.openstreetmap.org/search?country={country}&city={city}&street={street}&format=json&limit={limit}";
            
            var response = await QueryAsync(HttpMethod.Get, null!, url);
            var des = JsonConvert.DeserializeObject<IList<Coordinates>>(response);
            var ser = JsonConvert.SerializeObject(des);
            return ser;
        }

        [Route("/geo-objects")]
        [HttpGet]
        public async Task<string> GetGeoObjects(double lat = 58.1700039, double lon = 92.5283819, int radiusMeters = 100, int count = 10, string language = "ru")
            =>  JsonConvert.SerializeObject(await ShowObjectsByCoordinates(lat, lon, radiusMeters, count, language));

        [Route("/geo-objects-min")]
        [HttpGet]
        public async Task<string> GetGeoObjects(double lat = 58.1700039, double lon = 92.5283819)
            => JsonConvert.SerializeObject(await ShowObjectsByCoordinates(lat, lon));

        [Route("/geo-city")]
        [HttpGet]
        public  ValueTask<string> GetGeoSity(double lat = 58.1700039, double lon = 92.5283819, int radiusMeters = 100, int count = 1, string language = "ru")
            => new(JsonConvert.SerializeObject(ShowObjectsByCoordinates(lat, lon, radiusMeters, count, language).Result.Select(x => x.City)));

        [Route("/geo-street")]
        [HttpGet]
        public  ValueTask<string> GetGeoStreet(double lat = 58.1700039, double lon = 92.5283819, int radiusMeters = 100, int count = 1, string language = "ru")
            => new(JsonConvert.SerializeObject(ShowObjectsByCoordinates(lat, lon, radiusMeters, count, language).Result.Select(x => x.StreetType)));



        private async Task<IEnumerable<GeoData>> ShowObjectsByCoordinates(double lat = 58.1700039,
            double lon = 92.5283819, int radiusMeters = 100,
            int count = 10, string language = "ru")
        {
            GeoInfo geoInfo = new()
            {
                Lat = lat,
                Lon = lon,
                Count = count,
                RadiusMeters = radiusMeters,
                Language = language
            };
            IEnumerable<GeoData> data = new List<GeoData>();

            var response = await QueryAsync(HttpMethod.Post, geoInfo, BaseUrl);
            var responseDes = JsonConvert.DeserializeObject<GeoSuggestions<GeoData>>(response);
            if (responseDes.Suggestions != null)
            {
                data = responseDes.Suggestions.Select(x => x.Data)!;
            }

            return data;

        }


        private async Task<string> QueryAsync(HttpMethod method, object body, string url)
        {

            var request = new HttpRequestMessage(method, url)
            {
                Content = method == HttpMethod.Post
                    ? new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
                    : null
            };

            DefaultRequestHeaders();

            var response = await _httpClient.SendAsync(request);

            string responseData = await response.Content.ReadAsStringAsync();

            Log.Information($"response QueryAsync() response='{responseData}'");

            return responseData;
        }


        private void DefaultRequestHeaders()
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla FIrefox 5.4");
        }

    }
}
