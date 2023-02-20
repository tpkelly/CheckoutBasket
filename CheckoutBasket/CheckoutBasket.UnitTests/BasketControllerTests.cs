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
        private readonly Mock<IBasketPriceCalculator> mockPriceCalculator;
        private readonly BasketController testController;

        public BasketControllerTests()
        {
            mockBasket = new Mock<IBasketManager>();
            mockPriceCalculator = new Mock<IBasketPriceCalculator>();
            testController = new BasketController(mockBasket.Object, mockPriceCalculator.Object);
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

        public void BasketControllerCanCalculateBasketCost()
        {
            // Given
            mockPriceCalculator
                .Setup(c => c.GetCost(It.IsAny<IEnumerable<BasketItem>>()))
                .Returns(50);

            // When
            var result = testController.GetCost();

            // Then
            Assert.Equal(50, result);
        }
    }
}
