using CheckoutBasket.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutBasket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly Dictionary<ItemType, int> items;

        public BasketController()
        {
            items = new Dictionary<ItemType, int>();
        }

        [HttpGet]
        public IEnumerable<BasketItem> Get()
        {
            return items.Select(item => new BasketItem(item.Key, item.Value));
        }

        [HttpGet("Cost", Name = "BasketCost")]
        public decimal GetCost()
        {
            return 0;
        }

        [HttpPost(Name = "Add")]
        public void Add(ItemType item, int quantity = 1)
        {
            if (items.ContainsKey(item))
            {
                items[item] += quantity;
            }
            else
            {
                items.Add(item, quantity);
            }
        }


        [HttpDelete(Name = "Remove")]
        public void Remove(ItemType item, int quantity = 1)
        {
            if (!items.ContainsKey(item))
            {
                throw new InvalidOperationException($"Item {item} is not in the basket");
            }

            if (items[item] < quantity)
            {
                throw new InvalidOperationException($"Trying to remove more of item {item} than exist in the basket");
            }

            items[item] -= quantity;
        }   
    }
}