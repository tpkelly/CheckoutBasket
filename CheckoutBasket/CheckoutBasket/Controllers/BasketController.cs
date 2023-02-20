using CheckoutBasket.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutBasket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        public BasketController()
        {
        }

        [HttpGet]
        public IEnumerable<BasketItem> Get()
        {
            return new List<BasketItem>();
        }

        [HttpGet(Name = "BasketCost")]
        public decimal GetCost()
        {
            return 0;
        }

        [HttpPost(Name = "Add")]
        public void Add(ItemType item, int quantity = 1)
        {
        }


        [HttpDelete(Name = "Remove")]
        public void Remove(ItemType item, int quantity = 1)
        {
        }
    }
}