using CheckoutBasket.Models;

namespace CheckoutBasket.Basket
{
    public class BasketManager : IBasketManager
    {
        private readonly IDictionary<ItemType, int> items = new Dictionary<ItemType, int>();

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

        public IEnumerable<BasketItem> Contents()
        {
            return items.Select(item => new BasketItem(item.Key, item.Value));
        }

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
