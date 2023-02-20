using CheckoutBasket.Basket;
using CheckoutBasket.Models;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace CheckoutBasket.UnitTests
{
    public class BasketPriceCalculatorTests
    {
        private readonly BasketPriceCalculator testCalculator;
        public BasketPriceCalculatorTests()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            testCalculator = new BasketPriceCalculator(config);
        }

        [Fact]
        public void BasketPriceOfEmptyBasketIsZero()
        {
            // Given
            var basket = new BasketItem[0];

            // When
            var result = testCalculator.GetCost(basket);

            // Then
            Assert.Equal(0, result);
        }

        [Fact]
        public void BasketPriceOfOneOfEachItem()
        {
            // Given
            var basket = new[]
            {
                new BasketItem(ItemType.A, 1),
                new BasketItem(ItemType.B, 1),
                new BasketItem(ItemType.C, 1),
                new BasketItem(ItemType.D, 1)
            };

            // When
            var result = testCalculator.GetCost(basket);

            // Then
            Assert.Equal(10+15+40+55, result);
        }

        [Fact]
        public void BasketPriceOfMultipleOfOneItem()
        {
            // Given
            var basket = new[]
            {
                new BasketItem(ItemType.A, 5)
            };

            // When
            var result = testCalculator.GetCost(basket);

            // Then
            Assert.Equal(50, result);
        }

        [Theory]
        [InlineData(3, 40)]
        [InlineData(5, 70)]
        [InlineData(6, 80)]
        public void BasketPriceWithPromotionOnItemB(int quantity, decimal expectedCost)
        {
            // Given
            var basket = new[]
            {
                new BasketItem(ItemType.B, quantity)
            };

            // When
            var result = testCalculator.GetCost(basket);

            // Then
            Assert.Equal(expectedCost, result);
        }

        [Theory]
        [InlineData(2, 82.5)]
        [InlineData(3, 137.5)]
        [InlineData(4, 165)]
        public void BasketPriceWithPromotionOnItemD(int quantity, decimal expectedCost)
        {
            // Given
            var basket = new[]
            {
                new BasketItem(ItemType.D, quantity)
            };

            // When
            var result = testCalculator.GetCost(basket);

            // Then
            Assert.Equal(expectedCost, result);
        }
    }
}
