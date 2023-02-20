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

        public BasketController(IBasketManager basketManager)
        {
            basket = basketManager;
        }

        [HttpGet]
        public IEnumerable<BasketItem> Get()
        {
            return basket.Contents();
        }

        [HttpGet("Cost", Name = "BasketCost")]
        public decimal GetCost()
        {
            return 0;
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