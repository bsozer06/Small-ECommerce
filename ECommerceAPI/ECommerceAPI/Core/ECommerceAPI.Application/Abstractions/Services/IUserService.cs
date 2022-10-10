using ECommerceAPI.Application.Dtos.User;
using ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto model);
        Task UpdateResfreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate); 
    }
}
    