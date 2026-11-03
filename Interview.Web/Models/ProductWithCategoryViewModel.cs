namespace Interview.Web.Models
{
    public class ProductWithCategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string CategoryName { get; set; }
    }
}