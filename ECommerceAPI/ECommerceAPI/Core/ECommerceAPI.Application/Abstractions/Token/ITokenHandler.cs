using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Dtos.Token CreateAccessToken(int second, AppUser appUser);
        string CreateRefreshToken();
    }
}
