using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.ViewModels.Baskets;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Persistance.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository, IBasketWriteRepository basketWriteRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketReadRepository = basketReadRepository;
            _basketWriteRepository = basketWriteRepository;
        }

        private async Task<Basket?> ContextUser()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                var user = await _userManager.Users
                    .Include(u => u.Baskets)
                    .FirstOrDefaultAsync(x => x.UserName == username);

                // join process
                var _basket = from basket in user?.Baskets
                              join order in _orderReadRepository.Table
                              on basket.Id equals order.Id into BasketOrders
                              from ba in BasketOrders.DefaultIfEmpty()
                              select new
                              {
                                  Basket = basket,
                                  Order = ba
                              };

                Basket? targetBasket = null;
                if (_basket.Any(b => b.Order is null))
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                else
                {
                    targetBasket = new();
                    user.Baskets.Add(targetBasket);
                }

                await _basketWriteRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception("Beklenmeyen bir hatayla karşılaşıldı...");

        }



        public async Task AddItemToBasketAsync(VM_Create_BasketItem basketItem)
        {
            var basket = await ContextUser();
            if (basket != null)
            {
               var _basketItem = await _basketItemReadRepository
                    .GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(basketItem.ProductId));
                if (_basketItem != null)
                    _basketItem.Quantity++;
                else
                {
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity,
                    });
                }

                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            var basket = await ContextUser();
            var basketResult = await _basketReadRepository.Table
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.Id == basket!.Id);

            return basketResult.BasketItems.ToList();
        }

        public async Task RemoveBasketItemsAsync(string basketItemId)
        {
            var basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }
            
        }

        public async Task UpdateQuantityAsync(VM_Update_BasketItem basketItem)
        {
            var _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
            if (basketItem != null)
            {
                _basketItem.Quantity = basketItem.Quantity;
                await _basketItemWriteRepository.SaveAsync();
            }
        }
    }
}
