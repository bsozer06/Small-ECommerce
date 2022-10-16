using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddTransient<IProductHubService, ProductHubService>();
            services.AddTransient<IOrderHubService, OrderHubService>();
            services.AddSignalR();
        }
    }
}
