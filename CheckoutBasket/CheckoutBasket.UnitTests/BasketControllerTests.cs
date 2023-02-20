using CheckoutBasket.Controllers;
using CheckoutBasket.Models;
using Xunit;

namespace CheckoutBasket.UnitTests
{
    public class BasketControllerTests
    {
        private readonly BasketController testController;

        public BasketControllerTests()
        {
            testController = new BasketController();
        }

        [Fact]
        public void BasketWithNoModificationsIsEmpty()
        {
            // When
            var result = testController.Get();

            // Then
            Assert.Empty(result);
        }

        [Fact]
        public void BasketWithAddedItemsIncludesThoseItems()
        {
            // Given
            testController.Add(ItemType.A, 2);
            testController.Add(ItemType.B, 1);

            // When
            var result = testController.Get();

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
            testController.Add(ItemType.A, 2);
            testController.Add(ItemType.B, 1);
            testController.Remove(ItemType.A, 1);

            // When
            var result = testController.Get();

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
            // Given
            Assert.Throws<InvalidOperationException>(() => testController.Remove(ItemType.A, 1));
        }
    }
}