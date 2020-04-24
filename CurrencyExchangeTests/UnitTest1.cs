using CurrencyExchangeApp;
using System;
using Xunit;

namespace CurrencyExchangeTests
{
    public class UnitTest1
    {
        [Fact]
        public void Program_WhenPathOrTextIsNull_Return_Exception()
        {
            // Arrange
            string path = "  ";
            string text = null;
            var ag = new ArgumentNullException();

            // Act
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => Program.SaveAsync(path, text)).GetAwaiter().GetResult();

            // Assert
            Assert.Equal(ex.Message, ag.Message);
        }
    }
}
