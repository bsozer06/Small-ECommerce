using ECommerceAPI.Application.Features.Commands.AppUser.FacebookLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using ECommerceAPI.Application.Features.Commands.AppUser.RefreshToken;
using ECommerceAPI.Application.Features.Commands.PasswordReset;
using ECommerceAPI.Application.Features.Commands.VerifyResetToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            var response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            var response = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }


        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            var response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest)
        {
            var response = await _mediator.Send(facebookLoginCommandRequest);
            return Ok(response);
        }

        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordResetAsync([FromBody] PasswordResetCommandRequest passwordResetCommandRequest)
        {
            var response = await _mediator.Send(passwordResetCommandRequest);
            return Ok(response);
        }

        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetTokenAsync([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
        {
            var response = await _mediator.Send(verifyResetTokenCommandRequest);
            return Ok(response);
        }

    }
}
