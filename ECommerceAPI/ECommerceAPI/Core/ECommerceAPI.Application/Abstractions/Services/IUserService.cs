using ECommerceAPI.Application.Dtos.User;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto model);
    }
}
