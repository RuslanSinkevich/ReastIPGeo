using Newtonsoft.Json;

namespace RestIPGeo.models
{
    public class GeoFound : Coordinates
    { 
    [JsonProperty("place_id")]
    public int PlaceId {get; set;}

    [JsonProperty("licence")]
    public string? Licence { get; set; }

    [JsonProperty("osm_type")] 
    public string? OsmType {get; set;}

    [JsonProperty("intosm_id")]
    public int IntosmId {get; set;}

    [JsonProperty("boundingbox")]
    public double[]? Boundingbox { get; set; }

    [JsonProperty("display_name")]
    public string? DisplayName { get; set; }

    [JsonProperty("class")]
    public string? ClassName { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("importance")]
    public double? Importance { get; set; }

    [JsonProperty("icon")]
    public string? Icon { get; set; }
    }
}
