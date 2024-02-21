using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarWarsUI.Models
{
    public class StarshipViewModel
    {

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; } = default!;

        [Required]
        [DisplayName("Model")]
        public string Model { get; set; }

        [Required]
        [DisplayName("Manufacturer")]
        public string Manufacturer { get; set; } = default!;

        [Required]
        [DisplayName("Consumables")]
        public string Consumables { get; set; } = default!;

        [Required]
        [DisplayName("Films")]
        public List<string>? Films { get; set; } = default!;
    }
}
