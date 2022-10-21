using MediatR;

namespace ECommerceAPI.Application.Features.Commands.PasswordReset
{
    public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
    }
}
