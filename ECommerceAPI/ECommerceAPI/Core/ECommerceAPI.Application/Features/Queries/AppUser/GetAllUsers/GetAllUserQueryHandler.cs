using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, GetAllUserQueryResponse>
    {
        private readonly IUserService _userService;

        public GetAllUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<GetAllUserQueryResponse> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsersAsync(request.Page, request.Size);

            return new()
            {
                Users = users,
                TotalUsersCount = _userService.TotalUsersCount
            };
        }

    }
}
