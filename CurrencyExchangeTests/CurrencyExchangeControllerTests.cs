using CurrencyExchange;
using System;
using Xunit;

namespace CurrencyExchangeTests
{
    public class CurrencyExchangeControllerTests
    {
        [Fact]
        public void CurrencyExchangeController_WhenPathOrTextIsNull_Exception()
        {
            // Arrange
            var controller = new CurrencyExchangeController();
            var saveAndReadController = new SaveAndReadDataController();
            string path = "  ";
            string text = null;
            var arg = new ArgumentNullException();

            // Act
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => saveAndReadController.SaveDateAsync(path, text)).GetAwaiter().GetResult();

            // Assert
            Assert.Equal(ex.GetType(), arg.GetType());
        }

        [Fact]
        public void CurrencyExchangeController_WhenIdIsIncorrect_False()
        {
            // Arrange
            var controller = new CurrencyExchangeController();
            var id = -1;

            // Act
            bool idIsExist = controller.IdIsExist(id);

            // Assert
            Assert.False(idIsExist);
        }
    }
}
