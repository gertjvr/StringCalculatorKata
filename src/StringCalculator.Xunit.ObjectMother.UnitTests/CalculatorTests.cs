using System;
using System.Linq;
using Shouldly;
using Xunit;

namespace StringCalculator.Xunit.ObjectMother.UnitTests
{
    public class CalculatorTests
    {
        [Fact]
        public void AddEmptyReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();

            var numbers = string.Empty;

            var actual = sut.Add(numbers);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void AddSingleNumberReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var expected = ObjectMother.Get<int>();

            var numbers = expected.ToString();

            var actual = sut.Add(numbers);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddTwoNumbersReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();

            var numbers = string.Join(",", x, y);

            var actual = sut.Add(numbers);
            Assert.Equal(x + y, actual);
        }

        [Fact]
        public void AddAnyAmountOfNumbersReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var count = ObjectMother.Get<int>();
            var generator = ObjectMother.GetList<int>();
            
            var intergers = generator.Take(count + 2).ToArray();
            var numbers = string.Join(",", intergers);

            var actual = sut.Add(numbers);

            var expected = intergers.Sum();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddWithLineBreakAndCommaAsDelimiterReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();
            var z = ObjectMother.Get<int>();

            var numbers = string.Format("{0}\n{1},{2}", x, y, z);

            sut.Add(numbers).ShouldBe(x + y + z);
        }

        [Fact]
        public void AddLineWithCustomDelimiterReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var charGenerator = ObjectMother.GetList<char>();
            var count = ObjectMother.Get<int>();
            var intGenerator = ObjectMother.GetList<int>();

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
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddLineWithNegativeNumberThrowsCorrectException()
        {
            var sut = ObjectMother.Get<Calculator>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();
            var z = ObjectMother.Get<int>();

            var numbers = string.Join(",", -x, y, -z);

            var e = Assert.Throws<ArgumentOutOfRangeException>(
                () => sut.Add(numbers));

            Assert.True(e.Message.StartsWith("Negatives not allowed."));
            Assert.True(e.Message.Contains((-x).ToString()));
            Assert.True(e.Message.Contains((-z).ToString()));
        }

        [Fact]
        public void AddIgnoresBigNumbersReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var smallSeed = ObjectMother.Get<int>();
            var bigSeed = ObjectMother.Get<int>();
            
            var x = Math.Min(smallSeed, 1000);
            var y = bigSeed + 1000;
            var numbers = string.Join(",", x, y);

            var actual = sut.Add(numbers);

            Assert.Equal(x, actual);
        }

        [Fact]
        public void AddLineWithCustomDelimiterStringReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var delimiter = ObjectMother.Get<string>();
            var count = ObjectMother.Get<int>();
            var intGenerator = ObjectMother.GetList<int>();

            var integers = intGenerator.Take(count).ToArray();
            var numbers = string.Format(
                "//[{0}]\n{1}",
                delimiter,
                string.Join(delimiter, integers));

            var actual = sut.Add(numbers);

            var expected = integers.Sum();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddLineWithMultipleCustomDelimiterStringsReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var delimiter1 = ObjectMother.Get<string>();
            var delimiter2 = ObjectMother.Get<string>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();
            var z = ObjectMother.Get<int>();

            var numbers = string.Format(
                "//[{0}][{1}]\n{2}{0}{3}{1}{4}",
                delimiter1,
                delimiter2,
                x,
                y,
                z);

            var actual = sut.Add(numbers);

            var expected = x + y + z;
            Assert.Equal(expected, actual);
        }
    }
}