namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IAuthorizationEndpointService
    {
        public Task AssignRoleEndpointService(string[] roles, string menu, string code, Type type);
        public Task<List<string>> GetRolesToEndpoint(string code, string menu);
    }
}
