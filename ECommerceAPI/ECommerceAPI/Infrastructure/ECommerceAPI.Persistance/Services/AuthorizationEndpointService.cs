using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistance.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        // reflaction kullanılmıs service
        readonly IApplicationService _applicationService;
        readonly IEndpointReadRepository _endpointReadRepository;
        readonly IEndpointWriteRepository _endpointWriteRepository;
        readonly IMenuReadRepository _menuReadRepository;
        readonly IMenuWriteRepository _menuWriteRepository;
        readonly RoleManager<AppRole> _roleManager;


        public AuthorizationEndpointService(
            IApplicationService applicationService,
            IEndpointReadRepository endpointReadRepository,
            IEndpointWriteRepository endpointWriteRepository,
            IMenuReadRepository menuReadRepository,
            IMenuWriteRepository menuWriteRepository,
            RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointService(string[] roles, string menu, string code, Type type)
        {
            var _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu
                };

                await _menuWriteRepository.AddAsync(_menu);

                await _menuWriteRepository.SaveAsync();
            }



            //var endpoint = await _endpointReadRepository.GetSingleAsync(e => e.Code == code);
            var endpoint = await _endpointReadRepository.Table
                .Include(e => e.Menu)
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Menu.Name == menu && x.Code == code);
            if (endpoint != null)
            {
                var action = _applicationService.GetAuthorizeDefinitionEndpoints(type)?
                    .FirstOrDefault(x => x.Name == menu)?.Actions.FirstOrDefault();

                endpoint = new()
                {
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Id = Guid.NewGuid(),
                    Menu = _menu
                };

                await _endpointWriteRepository.AddAsync(endpoint);
                await _endpointWriteRepository.SaveAsync();
            }

            foreach (var role in endpoint.Roles)
                endpoint.Roles.Remove(role);

            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
                endpoint.Roles.Add(role);

            await _endpointWriteRepository.SaveAsync();





        }

        public async Task<List<string>> GetRolesToEndpoint(string code, string menu)
        {
            var endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .Include(e => e.Menu)
                .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            if (endpoint != null)
                return endpoint.Roles.Select(r => r.Name).ToList();
            return null;
        }
    }
}
