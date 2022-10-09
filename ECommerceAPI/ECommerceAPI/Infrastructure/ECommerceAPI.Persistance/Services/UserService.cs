using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Persistance.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager; 
        }

        public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto model)
        {
            var result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.NameSurname
            }, model.Password);

            CreateUserResponseDto response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
            {
                response.Message = "User has been created successfuly!!";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code}-{error.Description}\n";
                }
            }

            return response;
        }


    }
}
