using ECommerceAPI.Application.Abstractions.Services.Authentications;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IAuthService : IExternalAuthentications, IInternalAuthentications
    {
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    }
}
