using Newtonsoft.Json;

namespace RestIPGeo.models
{
    public class Suggestion<T>
    {
        [JsonProperty("value")]
        public string? Value { get; set; }

        [JsonProperty("unrestricted_value")]
        public string? UnrestrictedValue { get; set; }

        [JsonProperty("data")]
        public T? Data { get; set; }
    }
}
