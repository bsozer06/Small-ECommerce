namespace ECommerceAPI.Domain.Entities
{
    // TPH approaches: Table Per Hierachy
    public class ProductImageFile : File
    {
        public ICollection<Product> Products { get; set; }
    }
}
