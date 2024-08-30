using System.Text;
using ConsoleAPP;
namespace Test.ConsoleTest
{
    public class FizzBuzzTests
    {
        [Fact]
        public void GetFizzBuzz_ReturnsFizzBuzz_WhenDivisibleBy3And5()
        {
            // Arrange
            var fizzBuzz = new FizzBuzz();

            // Act
            var result = fizzBuzz.GetFizzBuzz(15);

            // Assert
            Assert.Equal("FizzBuzz", result);
        }

        [Fact]
        public void GetFizzBuzz_ReturnsFizz_WhenDivisibleBy3()
        {
            // Arrange
            var fizzBuzz = new FizzBuzz();

            // Act
            var result = fizzBuzz.GetFizzBuzz(9);

            // Assert
            Assert.Equal("Fizz", result);
        }

        [Fact]
        public void GetFizzBuzz_ReturnsBuzz_WhenDivisibleBy5()
        {
            // Arrange
            var fizzBuzz = new FizzBuzz();

            // Act
            var result = fizzBuzz.GetFizzBuzz(10);

            // Assert
            Assert.Equal("Buzz", result);
        }

        [Fact]
        public void GetFizzBuzz_ReturnsNumber_WhenNotDivisibleBy3Or5()
        {
            // Arrange
            var fizzBuzz = new FizzBuzz();

            // Act
            var result = fizzBuzz.GetFizzBuzz(7);

            // Assert
            Assert.Equal("7", result);
        }

        [Fact]
        public void ListFizzBuzz_ReturnsCorrectSequence()
        {
            // Arrange
            var fizzBuzz = new FizzBuzz();
            var expected = new StringBuilder();
            for (int i = 1; i <= 100; i++)
            {
                expected.AppendLine(fizzBuzz.GetFizzBuzz(i));
            }

            // Act
            var result = fizzBuzz.ListFizzBuzz();

            // Assert
            Assert.Equal(expected.ToString(), result);
        }
    }
}