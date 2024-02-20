namespace APIModels.Models
{
    public class FilmViewResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public int Episode_id { get; set; }
        public string Opening_crawl { get; set; } = default!;
        public string Director { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public DateTime? Rlease_date { get; set; } = default!;
        public int[]? Planets { get; set; }
    }
}
