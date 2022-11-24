namespace ECommerceAPI.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUserQueryResponse
    {
        public object Users { get; set; }
        public int TotalUsersCount { get; set; }
    }
}