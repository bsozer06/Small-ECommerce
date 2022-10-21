using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Helpers;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

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
                NameSurname = model.NameSurname,
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


        public async Task UpdateResfreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();

        }


        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);                          // security stamp ezilir. !
                else
                    throw new PasswordChangeFailedException();
            }
        }

    }
}
