namespace APIModels.Models
{
    public class ItemsModel<T> where T : BaseModel
    {
        public int count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<T>? results { get; set; }
    }
}
