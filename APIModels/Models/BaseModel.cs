using Newtonsoft.Json;

namespace APIModels.Models
{
    public class BaseModel
    {
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("edited")]
        public DateTime Edited { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
