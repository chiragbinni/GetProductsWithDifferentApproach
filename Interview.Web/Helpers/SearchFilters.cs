namespace Interview.Web.Helpers
{
    public class SearchFilters
    {
        public string? Search { get; set; }
        public string? Sort { get; set; }
        public string? Direction { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
