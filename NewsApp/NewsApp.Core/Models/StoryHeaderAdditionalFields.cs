using Newtonsoft.Json;

namespace NewsApp.Core.Models
{
    public class StoryHeaderAdditionalFields
    {
        [JsonProperty(PropertyName = "trailText")]
        public string TrailText { get; set; }
        [JsonProperty(PropertyName = "thumbnail")]
        public string Thumbnail { get; set; }
    }
}