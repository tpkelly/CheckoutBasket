using CheckoutBasket.Models;

namespace CheckoutBasket.Basket
{
    public interface IBasketPriceCalculator
    {
        decimal GetCost(IEnumerable<BasketItem> basket);
    }
}
