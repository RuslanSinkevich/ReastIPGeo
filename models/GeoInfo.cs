using Newtonsoft.Json;

namespace RestIPGeo.models
{
    public class GeoInfo: Coordinates
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("radius_meters")]
        public int RadiusMeters { get; set; }

        [JsonProperty("language")]
        public string? Language { get; set; }
    }

}
