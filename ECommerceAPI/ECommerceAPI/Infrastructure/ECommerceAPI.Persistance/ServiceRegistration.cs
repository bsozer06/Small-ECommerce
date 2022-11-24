using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Authentications;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistance.Contexts;
using ECommerceAPI.Persistance.Repositories;
using ECommerceAPI.Persistance.Repositories.File;
using ECommerceAPI.Persistance.Repositories.InvoiceFile;
using ECommerceAPI.Persistance.Repositories.ProductImageFile;
using ECommerceAPI.Persistance.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql(Configurations.ConnectionString));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ECommerceAPIDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentications, AuthService>();
            services.AddScoped<IInternalAuthentications, AuthService>();

            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<ICompletedOrderReadRepository, CompletedOrderReadRepository>();
            services.AddScoped<ICompletedOrderWriteRepository, CompletedOrderWriteRepository>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();
            services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
            services.AddScoped<IMenuReadRepository, MenuReadRepository>();
            services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();

            services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();

        }
    }
}
