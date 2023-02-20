using CheckoutBasket.Models;

namespace CheckoutBasket.Basket
{
    public interface IBasketManager
    {
        void Add(ItemType item, int quantity = 1);
        void Remove(ItemType item, int quantity = 1);
        IEnumerable<BasketItem> Contents();
    }
}
