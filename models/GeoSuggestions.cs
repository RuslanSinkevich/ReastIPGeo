
using Newtonsoft.Json;

namespace RestIPGeo.models
{
    public class GeoSuggestions<T>
    {
        [JsonProperty("suggestions")]
        public IList<Suggestion<T>>? Suggestions;
    }
}
