using CheckoutBasket.Basket;
using CheckoutBasket.Models;
using Xunit;

namespace CheckoutBasket.UnitTests
{
    public class BasketManagerTests
    {
        private readonly BasketManager testBasket;

        public BasketManagerTests()
        {
            testBasket = new BasketManager();
        }

        [Fact]
        public void BasketWithNoModificationsIsEmpty()
        {
            // When
            var result = testBasket.Contents();

            // Then
            Assert.Empty(result);
        }

        [Fact]
        public void BasketWithAddedItemsIncludesThoseItems()
        {
            // Given
            testBasket.Add(ItemType.A, 2);
            testBasket.Add(ItemType.B, 1);

            // When
            var result = testBasket.Contents();

            // Then
            var expectedResult = new[]
            {
                new BasketItem(ItemType.A, 2),
                new BasketItem(ItemType.B, 1)
            };
            Assert.Equivalent(expectedResult, result);
        }

        [Fact]
        public void BasketWithRemovedItemsDoesNotIncludeThoseItems()
        {
            // Given
            testBasket.Add(ItemType.A, 2);
            testBasket.Add(ItemType.B, 1);
            testBasket.Remove(ItemType.A, 1);

            // When
            var result = testBasket.Contents();

            // Then
            var expectedResult = new[]
            {
                new BasketItem(ItemType.A, 1),
                new BasketItem(ItemType.B, 1)
            };
            Assert.Equivalent(expectedResult, result);
        }

        [Fact]
        public void BasketWithNoItemsCannotRemoveItem()
        {
            Assert.Throws<InvalidOperationException>(() => testBasket.Remove(ItemType.A, 1));
        }

        [Fact]
        public void BasketCannotRemoveMoreItemsThanItHas()
        {
            // Given
            testBasket.Add(ItemType.A, 1);

            // Then
            Assert.Throws<InvalidOperationException>(() => testBasket.Remove(ItemType.A, 2));
        }
    }
}