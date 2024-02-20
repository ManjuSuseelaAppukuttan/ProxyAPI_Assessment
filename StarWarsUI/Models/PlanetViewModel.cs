using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarWarsUI.Models
{
    public class PlanetViewModel
    {
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; } = default!;

        [Required]
        [DisplayName("Rotation Period")]
        public string RotationPeriod { get; set; } = default!;

        [Required]
        [DisplayName("Orbital Period")]
        public string OrbitalPeriod { get; set; } = default!;

        [Required]
        [DisplayName("Diameter")]
        public string Diameter { get; set; } = default!;

        [Required]
        [DisplayName("Climate")]
        public string Climate { get; set; } = default!;

        [Required]
        [DisplayName("Gravity")]
        public string Gravity { get; set; } = default!;

        [Required]
        [DisplayName("Terrain")]
        public string Terrain { get; set; } = default!;

        [Required]
        [DisplayName("SurfaceWater")]
        public string SurfaceWater { get; set; } = default!;

        [Required]
        [DisplayName("Population")]
        public string Population { get; set; } = default!;

        [Required]
        [DisplayName("Residents")]
        public List<string>? Residents { get; set; }

        [Required]
        [DisplayName("Films")]
        public List<string>? Films { get; set; }
    }
}
