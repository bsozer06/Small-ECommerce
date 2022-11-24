using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistance.Contexts;

namespace ECommerceAPI.Persistance.Repositories
{
    public class EndpointWriteRepository : WriteRepository<Endpoint>, IEndpointWriteRepository
    {
        public EndpointWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
