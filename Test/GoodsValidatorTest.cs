using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania2.Test
{
    public class GoodsValidatorTest
    {
        [Fact]
        public void Validate_ShouldReturnSuccessfulResult_WhenGoodsIsValid()
        {
            // Arrange
            var goods = new Goods
            {
                Name = "Nazwa",
                Price = 100.0
            };

            var validator = new GoodsValidator();

            // Act
            var result = validator.Validate(goods);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("ok", result.Message);
            Assert.Equal(goods, result.obj);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("    ")]
        public void Validate_ShouldReturnFailedResult_WhenNameIsMissing(string myData)
        {
            // Arrange
            var goods = new Goods
            {
                Name = myData, // Pusta nazwa
                Price = 100.0
            };

            var validator = new GoodsValidator();

            // Act
            var result = validator.Validate(goods);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Brak wprowadzonej nazwy towaru", result.Message);
        }

        [Fact]
        public void Validate_ShouldReturnFailedResult_WhenPriceIsNegative()
        {
            // Arrange
            var goods = new Goods
            {
                Name = "Nazwa",
                Price = -50.0 // Ujemna cena
            };

            var validator = new GoodsValidator();

            // Act
            var result = validator.Validate(goods);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Nieprawidłowa cena towaru", result.Message);
        }


        [Fact]
        public void Validate_ShouldReturnFailedResult_WhenPriceIsZero()
        {
            // Arrange
            var goods = new Goods
            {
                Name = "Nazwa",
                Price = 0
            };

            var validator = new GoodsValidator();

            // Act
            var result = validator.Validate(goods);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Nieprawidłowa cena. Cena nie może równać się 0", result.Message);
        }

        [Fact]
        public void Validate_ShouldReturnFailedResult_WhenPriceIsNull()
        {
            // Arrange
            var goods = new Goods
            {
                Name = "Nazwa",
                Price = null // dodałem ? w modelu
            };

            var validator = new GoodsValidator();

            // Act
            var result = validator.Validate(goods);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Cena nie może być null", result.Message);
        }

    }

}
