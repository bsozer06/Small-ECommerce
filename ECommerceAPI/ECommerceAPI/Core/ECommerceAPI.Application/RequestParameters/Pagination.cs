namespace ECommerceAPI.Application.RequestParameters
{
    // class veya struct 'da olabilir
    public record Pagination
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
