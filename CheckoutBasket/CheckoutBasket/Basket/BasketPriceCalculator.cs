using CheckoutBasket.Models;

namespace CheckoutBasket.Basket
{
    public class BasketPriceCalculator : IBasketPriceCalculator
    {
        public decimal GetCost(IEnumerable<BasketItem> basket)
        {
            return 0;
        }
    }
}
