using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace StringCalculator.ObjectMother.NUnit.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void AddEmptyReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();

            var numbers = string.Empty;

            var actual = sut.Add(numbers);
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void AddSingleNumberReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var expected = ObjectMother.Get<int>();

            var numbers = expected.ToString();

            var actual = sut.Add(numbers);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddTwoNumbersReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();

            var numbers = string.Join(",", x, y);

            var actual = sut.Add(numbers);
            Assert.AreEqual(x + y, actual);
        }

        [Test]
        public void AddAnyAmountOfNumbersReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var count = ObjectMother.Get<int>();
            var generator = ObjectMother.GetList<int>();
            
            var intergers = generator.Take(count + 2).ToArray();
            var numbers = string.Join(",", intergers);

            var actual = sut.Add(numbers);

            var expected = intergers.Sum();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddWithLineBreakAndCommaAsDelimiterReturnsCorrectResult()
        {
            var sut = ObjectMother.Get<Calculator>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();
            var z = ObjectMother.Get<int>();

            var numbers = string.Format("{0}\n{1},{2}", x, y, z);

            var actual = sut.Add(numbers);
            Assert.AreEqual(x + y + z, actual);
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
                .Where(c => c != '-')
                .First();

            var integers = intGenerator.Take(count).ToArray();
            var numbers = string.Format(
                "//{0}\n{1}",
                delimiter,
                string.Join(delimiter.ToString(), integers));

            var actual = sut.Add(numbers);

            var expected = integers.Sum();
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void AddLineWithNegativeNumberThrowsCorrectException()
        {
            var sut = ObjectMother.Get<Calculator>();
            var x = ObjectMother.Get<int>();
            var y = ObjectMother.Get<int>();
            var z = ObjectMother.Get<int>();

            var numbers = string.Join(",", -x, y, -z);

            var e = Assert.Throws<ArgumentOutOfRangeException>(
                () => sut.Add(numbers));

            Assert.IsTrue(e.Message.StartsWith("Negatives not allowed."));
            Assert.IsTrue(e.Message.Contains((-x).ToString()));
            Assert.IsTrue(e.Message.Contains((-z).ToString()));
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

            var actual = sut.Add(numbers);

            Assert.AreEqual(x, actual);
        }

        [Test]
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
            Assert.AreEqual(expected, actual);
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

            var numbers = string.Format(
                "//[{0}][{1}]\n{2}{0}{3}{1}{4}",
                delimiter1,
                delimiter2,
                x,
                y,
                z);

            var actual = sut.Add(numbers);

            var expected = x + y + z;
            Assert.AreEqual(expected, actual);
        }
    }
}