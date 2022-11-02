using ECommerceAPI.Application.Dtos.Configuration;

namespace ECommerceAPI.Application.Abstractions.Services.Configurations
{
    public interface IApplicationService
    {
        List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
    }
}
