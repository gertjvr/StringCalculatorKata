using System;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Shouldly;

namespace StringCalculator.NUnit.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test, CalculatorTestConventions]
        public void AddEmptyReturnsCorrectResult(
            Calculator sut)
        {
            var numbers = "";
            var actual = sut.Add(numbers);
            Assert.AreEqual(0, actual);
        }

        [Test, CalculatorTestConventions]
        public void AddSingleNumberReturnsCorrectResult(
            Calculator sut,
            int expected)
        {
            var numbers = expected.ToString();
            var actual = sut.Add(numbers);
            actual.ShouldBe(expected);
        }

        [Test, CalculatorTestConventions]
        public void AddTwoNumbersReturnsCorrectResult(
            Calculator sut,
            int x,
            int y)
        {
            var numbers = string.Join(",", x, y);
            var actual = sut.Add(numbers);
            actual.ShouldBe(x + y);
        }

        [Test, CalculatorTestConventions]
        public void AddAnyAmountOfNumbersReturnsCorrectResult(
            Calculator sut,
            int count,
            Generator<int> generator)
        {
            var intergers = generator.Take(count + 2).ToArray();
            var numbers = string.Join(",", intergers);

            var actual = sut.Add(numbers);

            var expected = intergers.Sum();
            actual.ShouldBe(expected);
        }

        [Test, CalculatorTestConventions]
        public void AddWithLineBreakAndCommaAsDelimiterRetunrsCorrectResult(
            Calculator sut,
            int x,
            int y,
            int z)
        {
            var numbers = string.Format("{0}\n{1},{2}", x, y, z);
            var actual = sut.Add(numbers);
            
            var expected = x + y + z;
            actual.ShouldBe(expected);
        }

        [Test, CalculatorTestConventions]
        public void AddLineWithCustomDelimiterReturnsCorrectResult(
            Calculator sut,
            Generator<char> charGenerator,
            int count,
            Generator<int> intGenerator)
        {
            int dummy;
            var delimiter = charGenerator
                .Where(c => int.TryParse(c.ToString(), out dummy) == false)
                .Where(c => c != '-')
                .First();

            var integers = intGenerator.Take(count).ToArray();
            var numbers = string.Format(
                "//{0}\n{1}",
                delimiter,
                string.Join(delimiter.ToString(), integers));

            var actual = sut.Add(numbers);

            var expected = integers.Sum();
            actual.ShouldBe(expected);
        }

        [Test, CalculatorTestConventions]
        public void AddLineWithNegativeNumberThrowsCorrectException(
            Calculator sut,
            int x,
            int y,
            int z)
        {
            var numbers = string.Join(",", -x, y, -z);

            var e = Should.Throw<ArgumentOutOfRangeException>(
                () => sut.Add(numbers));

            e.Message.ShouldStartWith("Negatives not allowed.");
            e.Message.ShouldContain((-x).ToString());
            e.Message.ShouldContain((-z).ToString());
        }

        [Test, CalculatorTestConventions]
        public void AddIgnoresBigNumbers(
            Calculator sut,
            int smallSeed,
            int bigSeed)
        {
            var x = Math.Min(smallSeed, 1000);
            var y = bigSeed + 1000;
            var numbers = string.Join(",", x, y);

            var actual = sut.Add(numbers);

            actual.ShouldBe(x);
        }

        [Test, CalculatorTestConventions]
        public void AddLineWithCustomDelimiterStringReturnsCorrectResult(
            Calculator sut,
            string delimiter,
            int count,
            Generator<int> intGenerator)
        {
            var integers = intGenerator.Take(count).ToArray();
            var numbers = string.Format(
                "//[{0}]\n{1}",
                delimiter,
                string.Join(delimiter, integers));

            var actual = sut.Add(numbers);

            var expected = integers.Sum();
            actual.ShouldBe(expected);
        }

        [Test, CalculatorTestConventions]
        public void AddLineWithMultipleCustomDelimiterStringsReturnsCorrectResult(
            Calculator sut,
            string delimiter1,
            string delimiter2,
            int x,
            int y,
            int z)
        {
            var numbers = string.Format(
                "//[{0}][{1}]\n{2}{0}{3}{1}{4}",
                delimiter1,
                delimiter2,
                x,
                y,
                z);

            var actual = sut.Add(numbers);

            var expected = x + y + z;
            actual.ShouldBe(expected);
        }
    }
}