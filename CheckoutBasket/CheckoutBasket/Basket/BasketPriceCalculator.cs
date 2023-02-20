using CheckoutBasket.Models;

namespace CheckoutBasket.Basket
{
    public class BasketPriceCalculator : IBasketPriceCalculator
    {
        private readonly IConfiguration configuration;

        public BasketPriceCalculator(IConfiguration config)
        {
            configuration = config;
        }

        public decimal GetCost(IEnumerable<BasketItem> basket)
        {
            var basketCost = 0m;
            var promotions = configuration.GetSection("Promotions").Get<Promotion[]>();

            foreach ( var item in basket)
            {
                var perItemCost = configuration.GetValue<decimal>($"Pricing:{item.Type}");
                basketCost += item.Quantity * perItemCost;

                foreach (var promotion in promotions)
                {
                    if (promotion.SKU == item.Type.ToString())
                    {
                        // Integer division to round down to whole numbers
                        int timesToApply = item.Quantity / promotion.Quantity;
                        basketCost += promotion.Adjustment * timesToApply;
                    }
                }

            }

            return basketCost;
        }
    }
}
