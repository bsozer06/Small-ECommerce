using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.AppUser.GetRolesToUser
{
    public class GetRolesToUserQueryHandler : IRequestHandler<GetRolesToUserQueryRequest, GetRolesToUserQueryResponse>
    {
        readonly IUserService _userService;

        public GetRolesToUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<GetRolesToUserQueryResponse> Handle(GetRolesToUserQueryRequest request, CancellationToken cancellationToken)
        {
            return new()
            {
                UserRoles = await _userService.GetRolesToUserAsync(request.UserId)
            };
        }
    }
}
