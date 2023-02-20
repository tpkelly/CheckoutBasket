using CheckoutBasket.Basket;
using CheckoutBasket.Controllers;
using CheckoutBasket.Models;
using Moq;
using Xunit;

namespace CheckoutBasket.UnitTests
{
    public class BasketControllerTests
    {
        private readonly Mock<IBasketManager> mockBasket;
        private readonly BasketController testController;

        public BasketControllerTests()
        {
            mockBasket = new Mock<IBasketManager>();
            testController = new BasketController(mockBasket.Object);
        }

        [Fact]
        public void BasketControllerCanAddItems()
        {
            // When
            testController.Add(ItemType.C, 5);

            // Then
            mockBasket.Verify(basket => basket.Add(ItemType.C, 5), Times.Once);
        }

        [Fact]
        public void BasketControllerCanRemoveItems()
        {
            // When
            testController.Remove(ItemType.C, 5);

            // Then
            mockBasket.Verify(basket => basket.Remove(ItemType.C, 5), Times.Once);
        }

        [Fact]
        public void BasketControllerCanRetrieveContents()
        {
            // Given
            var contents = new[]
            {
                new BasketItem(ItemType.B, 3),
                new BasketItem(ItemType.D, 2)
            };
            mockBasket.Setup(b => b.Contents()).Returns(contents);

            // When
            var result = testController.Get();

            // Then
            Assert.Equivalent(contents, result);
        }
    }
}
