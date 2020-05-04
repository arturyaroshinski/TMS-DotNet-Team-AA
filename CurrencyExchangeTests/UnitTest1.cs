using CurrencyExchange;
using System;
using Xunit;

namespace CurrencyExchangeTests
{
    public class UnitTest1
    {
        [Fact]
        public void CurrencyExchangeController_WhenPathOrTextIsNull_Return_Exception()
        {
            // Arrange
            var controller = new CurrencyExchangeController();
            string path = "  ";
            string text = null;
            var arg = new ArgumentNullException();

            // Act
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => CurrencyExchangeController.SaveAsync(path, text)).GetAwaiter().GetResult();

            // Assert
            Assert.Equal(ex.GetType(), arg.GetType());
        }

        [Fact]
        public void CurrencyExchangeController_WhenIdIsIncorrect_Return_false()
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
