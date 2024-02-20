using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarWarsUI.Models
{
    public class FilmViewModel
    {
        [Required]
        [DisplayName("ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; } = default!;

        [Required]
        [DisplayName("Episode No")]
        public int Episode_id { get; set; }

        [Required]
        [DisplayName("Opening Crawl")]
        public string Opening_crawl { get; set; } = default!;

        [Required]
        [DisplayName("Directed By")]
        public string Director { get; set; } = default!;

        [Required]
        [DisplayName("Produced By")]
        public string Producer { get; set; } = default!;

        [Required]
        [DisplayName("Rlease Date")]
        public DateTime? Rlease_date { get; set; } = default!;
    }
}
