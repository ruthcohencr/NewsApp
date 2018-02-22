using Newtonsoft.Json;

namespace NewsApp.Core.Models
{
    public class SearchResult
    {
        [JsonProperty(PropertyName = "response")]
        public SearchResponse SearchResponse { get; set; }
    }
}