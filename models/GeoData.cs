using Newtonsoft.Json;

namespace RestIPGeo.models
{
    public class GeoData
    {
        [JsonProperty("country")]
        public string? Country { get; set; }
        
        [JsonProperty("region_with_type")]
        public string? RegionType { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("street_with_type")]
        public string? StreetType { get; set; }

        [JsonProperty("house")]
        public string? House { get; set; }

        [JsonProperty("flat")]
        public string? Flat { get; set; }

    }
}
