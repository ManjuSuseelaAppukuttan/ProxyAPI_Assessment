namespace APIModels.Models
{
    public class PlanetsViewResponseModel
    {
        public string Name { get; set; } = default!;
        public string RotationPeriod { get; set; } = default!;
        public string OrbitalPeriod { get; set; } = default!;
        public string Diameter { get; set; } = default!;
        public string Climate { get; set; } = default!;
        public string Gravity { get; set; } = default!;
        public string Terrain { get; set; } = default!;
        public string SurfaceWater { get; set; } = default!;
        public string Population { get; set; } = default!;
        public List<string>? Residents { get; set; }
        public List<string>? Films { get; set; }
    }
}
