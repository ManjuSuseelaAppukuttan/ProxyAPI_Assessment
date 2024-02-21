using Newtonsoft.Json;

namespace APIModels.Models
{
    public class FilmModel : BaseModel
    {
        [JsonProperty("title")]
        public string Title { get; set; } = default!;
        [JsonProperty("episode_id")]
        public int Episode_id { get; set; }
        public string Opening_crawl { get; set; } = default!;
        public string Director { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public string Rlease_date { get; set; } = default!;
        [JsonProperty("characters")]
        public List<string>? Characters { get; set; }
        [JsonProperty("planets")]
        public List<string>? Planets { get; set; }

        [JsonProperty("starships")]
        public List<string> Starships { get; set; }

    }
}
