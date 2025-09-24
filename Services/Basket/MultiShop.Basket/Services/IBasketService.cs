using MultiShop.Basket.Dtos;

namespace MultiShop.Basket.Services;

public interface IBasketService
{
    Task<BasketTotalDto> GetBasket(string UserId);
    Task SaveBasket(BasketTotalDto basketTotalDto);
    Task DeleteBasket(string UserId);
}