using System;
using System.Globalization;
using System.Linq;
using AutoFixture;
using TestStack.BDDfy;
using Xunit;

namespace StringCalculator.Xunit.BDDfy.UnitTests
{
    public class CalculatorTests : CalculatorOrchestration
    {
        [Theory, UseAutoFixtureToCreateParameters]
        public void AddEmptyReturnsCorrectResults(Calculator sut)
        {   
            var numbers = string.Empty;
            var expected = 0;
            
            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        [Theory, UseAutoFixtureToCreateParameters]
        public void AddSingleNumberReturnsCorrectResult(
            Calculator sut,
            int expected)
        {
            var numbers = expected.ToString(CultureInfo.InvariantCulture);

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        [Theory, UseAutoFixtureToCreateParameters]
        public void AddTwoNumbersReturnsCorrectResult(
            Calculator sut,
            int x,
            int y)
        {
            var numbers = string.Join(",", x, y).ToString(CultureInfo.InvariantCulture);
            var expected = x + y;

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        [Theory, UseAutoFixtureToCreateParameters]
        public void AddAnyAmountOfNumbersReturnsCorrectResult(
            Calculator sut,
            int count,
            Generator<int> generator)
        {
            var integers = generator.Take(count + 2).ToArray();
            var numbers = string.Join(",", integers);
            var expected = integers.Sum();

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        [Theory, UseAutoFixtureToCreateParameters]
        public void AddWithLineBreakAndCommaAsDelimiterRetunrsCorrectResult(
            Calculator sut,
            int x,
            int y,
            int z)
        {
            var numbers = $"{x}\n{y},{z}";
            var expected = x + y + z;

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        [Theory, UseAutoFixtureToCreateParameters]
        public void AddLineWithCustomDelimiterReturnsCorrectResult(
            Calculator sut,
            Generator<char> charGenerator,
            int count,
            Generator<int> intGenerator)
        {
            int dummy;
            var delimiter = charGenerator
                .Where(c => int.TryParse(c.ToString(), out dummy) == false)
                .First(c => c != '-');

            var integers = intGenerator.Take(count).ToArray();
            var numbers = $"//{delimiter}\n{string.Join(delimiter.ToString(), integers)}";
            var expected = integers.Sum();

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        [Theory, UseAutoFixtureToCreateParameters]
        public void AddLineWithNegativeNumberThrowsCorrectException(
            Calculator sut,
            int x,
            int y,
            int z)
        {
            var numbers = string.Join(",", -x, y, -z);

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculatedThrowsArgumentOutOfRangeException(numbers))
                .Then(t => t.ThenExceptionMessage($"Negatives not allowed. Found {-x},{-z}. (Parameter 'numbers')"))
                .BDDfy();
        }

        [Theory, UseAutoFixtureToCreateParameters]
        public void AddIgnoresBigNumbers(
            Calculator sut,
            int smallSeed,
            int bigSeed)
        {
            var x = Math.Min(smallSeed, 1000);
            var y = bigSeed + 1000;
            var numbers = string.Join(",", x, y);
            var expected = x;

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        [Theory, UseAutoFixtureToCreateParameters]
        public void AddLineWithCustomDelimiterStringReturnsCorrectResult(
            Calculator sut,
            string delimiter,
            int count,
            Generator<int> intGenerator)
        {
            var integers = intGenerator.Take(count).ToArray();
            var numbers = $"//[{delimiter}]\n{string.Join(delimiter, integers)}";
            var expected = integers.Sum();

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        [Theory, UseAutoFixtureToCreateParameters]
        public void AddLineWithMultipleCustomDelimiterStringsReturnsCorrectResult(
            Calculator sut,
            string delimiter1,
            string delimiter2,
            int x,
            int y,
            int z)
        {
            var numbers = $"//[{delimiter1}][{delimiter2}]\n{x}{delimiter1}{y}{delimiter2}{z}";
            var expected = x + y + z; 
            
            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }
    }
}