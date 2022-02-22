using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Xbehave;

namespace StringCalculator.Xunit.XBehave.UnitTests
{
    public class CalculatorTests
    {
        [Scenario, UseAutoFixtureToCreateParameters]
        public void AddEmptyReturnsCorrectResults(
            Calculator sut)
        {
            var result = () => 0;
            var numbers = string.Empty;
            var expected = 0;
            
            "Given a calculator"
                .x(() => {});

            "When the result is calculated"
                .x(() =>
                {
                    result = sut.Invoking(_ => _.Add(numbers));
                });

            "Then the expected result should be"
                .x(() =>
                {
                    result().Should().Be(expected);
                });
        }

        [Scenario, UseAutoFixtureToCreateParameters]
        public void AddSingleNumberReturnsCorrectResult(
            Calculator sut,
            int expected)
        {
            var result = () => 0;
            var numbers = string.Empty;
            
            "Given a calculator"
                .x(() =>
                {
                    numbers = expected.ToString();
                });

            "When the result is calculated"
                .x(() =>
                {
                    result = sut.Invoking(_ => _.Add(numbers));
                });

            "Then the expected result should be"
                .x(() =>
                {
                    result().Should().Be(expected);
                });
        }

        [Scenario, UseAutoFixtureToCreateParameters]
        public void AddTwoNumbersReturnsCorrectResult(
            Calculator sut,
            int x,
            int y)
        {
            var result = () => 0;
            var numbers = string.Empty;
            var expected = 0;
            
            "Given a calculator"
                .x(() =>
                {
                    numbers = string.Join(",", x, y);
                    expected = x + y;
                });

            "When the result is calculated"
                .x(() =>
                {
                    result = sut.Invoking(_ => _.Add(numbers));
                });

            "Then the expected result should be"
                .x(() =>
                {
                    result().Should().Be(expected);
                });
        }

        [Scenario, UseAutoFixtureToCreateParameters]
        public void AddAnyAmountOfNumbersReturnsCorrectResult(
            Calculator sut,
            int count,
            Generator<int> generator)
        {
            var result = () => 0;
            var numbers = string.Empty;
            var expected = 0;
            
            "Given a calculator"
                .x(() =>
                {
                    var integers = generator.Take(count + 2).ToArray();
                    
                    numbers = string.Join(",", integers);
                    expected = integers.Sum();
                });

            "When the result is calculated"
                .x(() =>
                {
                    result = sut.Invoking(_ => _.Add(numbers));
                });

            "Then the expected result should be"
                .x(() =>
                {
                    result().Should().Be(expected);
                });
        }

        [Scenario, UseAutoFixtureToCreateParameters]
        public void AddWithLineBreakAndCommaAsDelimiterReturnsCorrectResult(
            Calculator sut,
            int x,
            int y,
            int z)
        {   
            var result = () => 0;
            var numbers = string.Empty;
            var expected = 0;
            
            "Given a calculator"
                .x(() =>
                {
                    numbers = $"{x}\n{y},{z}";
                    expected = x + y + z;
                });

            "When the result is calculated"
                .x(() =>
                {
                    result = sut.Invoking(_ => _.Add(numbers));
                });

            "Then the expected result should be"
                .x(() =>
                {
                    result().Should().Be(expected);
                });
        }

        [Scenario, UseAutoFixtureToCreateParameters]
        public void AddLineWithCustomDelimiterReturnsCorrectResult(
            Calculator sut,
            Generator<char> charGenerator,
            int count,
            Generator<int> intGenerator)
        {
            var result = () => 0;
            var numbers = string.Empty;
            var expected = 0;
            
            "Given a calculator"
                .x(() =>
                {
                    int dummy;
                    var delimiter = charGenerator
                        .Where(c => int.TryParse(c.ToString(), out dummy) == false)
                        .First(c => c != '-');
                    
                    var integers = intGenerator.Take(count).ToArray();
                    
                    numbers = $"//{delimiter}\n{string.Join(delimiter.ToString(), integers)}";
                    expected = integers.Sum();
                });

            "When the result is calculated"
                .x(() =>
                {
                    result = sut.Invoking(_ => _.Add(numbers));
                });

            "Then the expected result should be"
                .x(() =>
                {
                    result().Should().Be(expected);
                });
        }

        [Scenario, UseAutoFixtureToCreateParameters]
        public void AddLineWithNegativeNumberThrowsCorrectException(
            Calculator sut,
            int x,
            int y,
            int z)
        {
            var result = () => 0;
            var numbers = string.Empty;

            "Given a calculator"
                .x(() =>
                {
                    numbers = string.Join(",", -x, y, -z);
                });

            "When the result is calculated"
                .x(() =>
                {
                    result = sut.Invoking(_ => _.Add(numbers));
                });

            "Then throws argument out of range exception"
                .x(() =>
                {
                    result.Should()
                        .Throw<ArgumentOutOfRangeException>()
                        .WithMessage($"Negatives not allowed. Found {-x},{-z}. (Parameter 'numbers')");
                });
        }

        [Scenario, UseAutoFixtureToCreateParameters]
        public void AddIgnoresBigNumbers(
            Calculator sut,
            int smallSeed,
            int bigSeed)
        {
            var result = () => 0;
            var numbers = string.Empty;
            var expected = 0;
            
            "Given a calculator"
                .x(() =>
                {
                    var x = Math.Min(smallSeed, 1000);
                    var y = bigSeed + 1000;
                    
                    numbers = string.Join(",", x, y);
                    expected = x;
                });

            "When the result is calculated"
                .x(() =>
                {
                    result = sut.Invoking(_ => _.Add(numbers));
                });

            "Then the expected result should be"
                .x(() =>
                {
                    result().Should().Be(expected);
                });
        }

        [Scenario, UseAutoFixtureToCreateParameters]
        public void AddLineWithCustomDelimiterStringReturnsCorrectResult(
            Calculator sut,
            string delimiter,
            int count,
            Generator<int> intGenerator)
        {    
            var result = () => 0;
            var numbers = string.Empty;
            var expected = 0;
            
            "Given a calculator"
                .x(() =>
                {
                    var integers = intGenerator.Take(count).ToArray();
                    
                    numbers = $"//[{delimiter}]\n{string.Join(delimiter, integers)}";
                    expected = integers.Sum();
                });

            "When the result is calculated"
                .x(() =>
                {
                    result = sut.Invoking(_ => _.Add(numbers));
                });

            "Then the expected result should be"
                .x(() =>
                {
                    result().Should().Be(expected);
                });
        }

        [Scenario, UseAutoFixtureToCreateParameters]
        public void AddLineWithMultipleCustomDelimiterStringsReturnsCorrectResult(
            Calculator sut,
            string delimiter1,
            string delimiter2,
            int x,
            int y,
            int z)
        {
            var result = () => 0;
            var numbers = string.Empty;
            var expected = 0;
            
            "Given a calculator"
                .x(() =>
                {
                    numbers = $"//[{delimiter1}][{delimiter2}]\n{x}{delimiter1}{y}{delimiter2}{z}";
                    expected = x + y + z;
                });

            "When the result is calculated"
                .x(() =>
                {
                    result = sut.Invoking(_ => _.Add(numbers));
                });

            "Then the expected result should be"
                .x(() =>
                {
                    result().Should().Be(expected);
                });
        }
    }
}
