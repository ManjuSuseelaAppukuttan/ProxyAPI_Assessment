using Newtonsoft.Json;

namespace APIModels.Models
{
    public class PlanetsModel:BaseModel
    {
        [JsonProperty("name")]
        public string Name { get; set; } = default!;

        [JsonProperty("rotation_period")]
        public string RotationPeriod { get; set; } = default!;

        [JsonProperty("orbital_period")]
        public string OrbitalPeriod { get; set; } = default!;

        [JsonProperty("diameter")]
        public string Diameter { get; set; } = default!;

        [JsonProperty("climate")]
        public string Climate { get; set; } = default!;

        [JsonProperty("gravity")]
        public string Gravity { get; set; } = default!;

        [JsonProperty("terrain")]
        public string Terrain { get; set; } = default!;

        [JsonProperty("surface_water")]
        public string SurfaceWater { get; set; } = default!;

        [JsonProperty("population")]
        public string Population { get; set; } = default!;

        [JsonProperty("residents")]
        public List<string>? Residents { get; set; }

        [JsonProperty("films")]
        public List<string>? Films { get; set; }
    }
}
