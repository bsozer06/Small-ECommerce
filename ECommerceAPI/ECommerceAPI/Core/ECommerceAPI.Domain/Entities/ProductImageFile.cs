namespace ECommerceAPI.Domain.Entities
{
    // TPH approaches: Table Per Hierachy
    public class ProductImageFile : File
    {
        public bool Showcase { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
