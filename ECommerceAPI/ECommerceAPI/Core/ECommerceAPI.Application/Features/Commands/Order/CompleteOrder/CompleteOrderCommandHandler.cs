using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Order;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IMailService _mailService;

        public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }


        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            (bool succeeded, CompletedOrderDto dto) = await _orderService.CompleteOrderAsync(request.Id);
            if (succeeded)
                await _mailService.SendCompletedOrderMailAsync(
                    dto.EMail,
                    dto.OrderCode,
                    dto.OrderDate,
                    dto.Username,
                    );
            return new ();
        }
    }
}
