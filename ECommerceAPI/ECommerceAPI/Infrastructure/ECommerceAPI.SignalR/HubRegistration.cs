using ECommerceAPI.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace ECommerceAPI.SignalR
{
    public static class HubRegistration
    {
        /// <summary>
        /// extensiton hub işlemleri icin
        /// </summary>
        /// <param name="application"></param>
        public static void MapHubs(this WebApplication application)
        {
            application.MapHub<ProductHub>("/products-hub");
        }

    }
}
