namespace ECommerceAPI.Application.Dtos.Order
{
    public class CompletedOrderDto
    {
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Username { get; set; }
        //public string UserSurname { get; set; }
        public string EMail { get; set; }
    }
}
