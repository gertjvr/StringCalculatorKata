using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace StringCalculator.NUnit.ObjectMother.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void AddEmptyReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();

            var numbers = string.Empty;
            var expected = 0;

            sut.Add(numbers).Should().Be(expected);
        }

        [Test]
        public void AddSingleNumberReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var expected = ObjectMother.Get<int>();

            var numbers = expected.ToString();

            sut.Add(numbers).Should().Be(expected);
        }

        [Test]
        public void AddTwoNumbersReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();

            var numbers = string.Join(",", x, y);
            var expected = x + y;

            sut.Add(numbers).Should().Be(expected);
        }

        [Test]
        public void AddAnyAmountOfNumbersReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var count = ObjectMother.Get<int>();
            var generator = ObjectMother.GetList<int>();
            
            var integers = generator.Take(count + 2).ToArray();
            var numbers = string.Join(",", integers);
            var expected = integers.Sum();

            sut.Add(numbers).Should().Be(expected);
        }

        [Test]
        public void AddWithLineBreakAndCommaAsDelimiterReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();
            var z = ObjectMother.Get<int>();

            var numbers = $"{x}\n{y},{z}";
            var expected = x + y + z;

            sut.Add(numbers).Should().Be(expected);
        }

        [Test]
        public void AddLineWithCustomDelimiterReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var charGenerator = ObjectMother.GetList<char>();
            var count = ObjectMother.Get<int>();
            var intGenerator = ObjectMother.GetList<int>();

            int dummy;
            var delimiter = charGenerator
                .Where(c => int.TryParse(c.ToString(), out dummy) == false)
                .First(c => c != '-');

            var integers = intGenerator.Take(count).ToArray();
            var numbers = $"//{delimiter}\n{string.Join(delimiter.ToString(), integers)}";
            var expected = integers.Sum();

            sut.Add(numbers).Should().Be(expected);
        }

        [Test]
        public void AddLineWithNegativeNumberThrowsCorrectException()
        {
            var sut = ObjectMother.Get<Calculator>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();
            var z = ObjectMother.Get<int>();

            var numbers = string.Join(",", -x, y, -z);

            sut.Invoking(_ => _.Add(numbers))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"Negatives not allowed. Found {-x},{-z}. (Parameter 'numbers')");
        }

        [Test]
        public void AddIgnoresBigNumbersReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var smallSeed = ObjectMother.Get<int>();
            var bigSeed = ObjectMother.Get<int>();
            
            var x = Math.Min(smallSeed, 1000);
            var y = bigSeed + 1000;
            var numbers = string.Join(",", x, y);
            var expected = x;

            sut.Add(numbers).Should().Be(expected);
        }

        [Test]
        public void AddLineWithCustomDelimiterStringReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var delimiter = ObjectMother.Get<string>();
            var count = ObjectMother.Get<int>();
            var intGenerator = ObjectMother.GetList<int>();

            var integers = intGenerator.Take(count).ToArray();
            var numbers = $"//[{delimiter}]\n{string.Join(delimiter, integers)}";
            var expected = integers.Sum();

            sut.Add(numbers).Should().Be(expected);
        }

        [Test]
        public void AddLineWithMultipleCustomDelimiterStringsReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var delimiter1 = ObjectMother.Get<string>();
            var delimiter2 = ObjectMother.Get<string>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();
            var z = ObjectMother.Get<int>();

            var numbers = $"//[{delimiter1}][{delimiter2}]\n{x}{delimiter1}{y}{delimiter2}{z}";
            var expected = x + y + z;

            sut.Add(numbers).Should().Be(expected);
        }
    }
}