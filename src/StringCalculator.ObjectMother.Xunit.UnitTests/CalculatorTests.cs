using System;
using System.Linq;
using Shouldly;
using Xunit;

namespace StringCalculator.ObjectMother.Xunit.UnitTests
{
    public class CalculatorTests
    {
        [Fact]
        public void AddEmptyReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();

            var numbers = string.Empty;

            sut.Add(numbers).ShouldBe(0);
        }

        [Fact]
        public void AddSingleNumberReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var expected = ObjectMother.Get<int>();

            var numbers = expected.ToString();

            sut.Add(numbers).ShouldBe(expected);
        }

        [Fact]
        public void AddTwoNumbersReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();

            var numbers = string.Join(",", x, y);

            sut.Add(numbers).ShouldBe(x + y);
        }

        [Fact]
        public void AddAnyAmountOfNumbersReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var count = ObjectMother.Get<int>();
            var generator = ObjectMother.GetList<int>();
            
            var intergers = generator.Take(count + 2).ToArray();
            var numbers = string.Join(",", intergers);

            sut.Add(numbers).ShouldBe(intergers.Sum());
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

            sut.Add(numbers).ShouldBe(integers.Sum());

        }

        [Fact]
        public void AddLineWithNegativeNumberThrowsCorrectException()
        {
            var sut = ObjectMother.Get<Calculator>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();
            var z = ObjectMother.Get<int>();

            var numbers = string.Join(",", -x, y, -z);

            var e = Should.Throw<ArgumentOutOfRangeException>(
                () => sut.Add(numbers));

            e.Message.ShouldStartWith("Negatives not allowed.");
            e.Message.ShouldContain((-x).ToString());
            e.Message.ShouldContain((-z).ToString());
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

            sut.Add(numbers).ShouldBe(x);
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

            sut.Add(numbers).ShouldBe(integers.Sum());
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

            sut.Add(numbers).ShouldBe(x + y + z);
        }
    }
}