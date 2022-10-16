using ECommerceAPI.Application.Dtos.Order;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrder createOrder);
    }
}
