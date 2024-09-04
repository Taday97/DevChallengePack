using ConsoleAPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ConsoleTest
{
    public class PrimeNumbersTests
    {
        [Fact]
        public void GetPrimeNumbers_ReturnsCorrectSequence_ForFirstFivePrimes()
        {
            // Arrange
            var primeNumbers = new PrimeNumbers();
            var expected = "2,3,5,7,11";

            // Act
            var result = primeNumbers.GetPrimeNumbers(5);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetPrimeNumbers_ReturnsEmptyString_ForZeroPrimes()
        {
            // Arrange
            var primeNumbers = new PrimeNumbers();
            var expected ="";

            // Act
            var result = primeNumbers.GetPrimeNumbers(0);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, false)]
        [InlineData(5, true)]
        [InlineData(10, false)]
        [InlineData(13, true)]
        public void IsPrime_ReturnsCorrectResult(int number, bool expected)
        {
            // Act
            var primeNumbers = new PrimeNumbers();
            var result = primeNumbers.IsPrime(number);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}