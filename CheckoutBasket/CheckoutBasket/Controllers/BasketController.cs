using CheckoutBasket.Basket;
using CheckoutBasket.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutBasket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketManager basket;
        private readonly IBasketPriceCalculator pricing;

        public BasketController(IBasketManager basketManager, IBasketPriceCalculator priceCalculator)
        {
            basket = basketManager;
            pricing = priceCalculator;
        }

        [HttpGet]
        public IEnumerable<BasketItem> Get()
        {
            return basket.Contents();
        }

        [HttpGet("Cost", Name = "BasketCost")]
        public decimal GetCost()
        {
            return pricing.GetCost(basket.Contents());
        }

        [HttpPost(Name = "Add")]
        public void Add(ItemType item, int quantity = 1)
        {
            basket.Add(item, quantity);
        }

        [HttpDelete(Name = "Remove")]
        public void Remove(ItemType item, int quantity = 1)
        {
            basket.Remove(item, quantity);
        }   
    }
}