using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace StringCalculator.NUnit.SpecFor.UnitTests
{
    public abstract class CalculatorSpecFor : SpecFor<Calculator>
    {
        protected string Numbers { get; set; } = string.Empty;

        protected int Expected { get; set; }

        protected int Result { get; set; }
    }

    [TestFixture]
    public class AddEmpty : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            Numbers = string.Empty;
            Expected = 0;

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        [Then]
        public void ReturnsCorrectResult()
        {
            Result.Should().Be(Expected);
        }
    }

    [TestFixture]
    public class AddSingleNumber : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            Expected = Fixture.Create<int>();
            Numbers = Expected.ToString();

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        [Then]
        public void ReturnsCorrectResult()
        {
            Result.Should().Be(Expected);
        }
    }

    [TestFixture]
    public class AddTwoNumbers : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            var x = Fixture.Create<int>();
            var y = Fixture.Create<int>();

            Numbers = string.Join(",", x, y);
            Expected = x + y;

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        [Then]
        public void ReturnsCorrectResult()
        {
            Result.Should().Be(Expected);
        }
    }

    [TestFixture]
    public class AddAnyAmountOfNumbers : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            var generator = Fixture.Create<Generator<int>>();
            var count = Fixture.Create<int>();
            var integers = generator.Take(count + 2).ToArray();

            Numbers = string.Join(",", integers);
            Expected = integers.Sum();

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        [Then]
        public void ReturnsCorrectResult()
        {
            Result.Should().Be(Expected);
        }
    }

    [TestFixture]
    public class AddWithLineBreakAndCommaAsDelimiter : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            var x = Fixture.Create<int>();
            var y = Fixture.Create<int>();
            var z = Fixture.Create<int>();

            Numbers = $"{x}\n{y},{z}";
            Expected = x + y + z;

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        [Then]
        public void ReturnsCorrectResult()
        {
            Result.Should().Be(Expected);
        }
    }

    [TestFixture]
    public class AddLineWithCustomDelimiter : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            var charGenerator = Fixture.Create<Generator<char>>();
            var count = Fixture.Create<int>();
            var intGenerator = Fixture.Create<Generator<int>>();

            int dummy;
            var delimiter = charGenerator
                .Where(c => int.TryParse(c.ToString(), out dummy) == false)
                .First(c => c != '-');

            var integers = intGenerator.Take(count).ToArray();

            Numbers = $"//{delimiter}\n{string.Join(delimiter.ToString(), integers)}";
            Expected = integers.Sum();

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        [Then]
        public void ReturnsCorrectResult()
        {
            Result.Should().Be(Expected);
        }
    }

    [TestFixture]
    public class AddLineWithNegativeNumber : CalculatorSpecFor
    {
        private int _x;
        private int _y;
        private int _z;

        protected override Calculator Given()
        {
            _x = Fixture.Create<int>();
            _y = Fixture.Create<int>();
            _z = Fixture.Create<int>();

            Numbers = string.Join(",", -_x, _y, -_z);
            
            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {   
        }

        [Then]
        public void ThrowsCorrectException()
        {
            Subject.Invoking(_ => _.Add(Numbers))
                .Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"Negatives not allowed. Found {-_x},{-_z}. (Parameter 'numbers')");
        }
    }

    [TestFixture]
    public class AddIgnoresBigNumbers : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            var smallSeed = Fixture.Create<int>();
            var bigSeed = Fixture.Create<int>();

            var x = Math.Min(smallSeed, 1000);
            var y = bigSeed + 1000;
            
            Numbers = string.Join(",", x, y);
            Expected = x;

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        [Then]
        public void ReturnsCorrectResult()
        {
            Result.Should().Be(Expected);
        }
    }

    [TestFixture]
    public class AddLineWithCustomDelimiterString : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            var delimiter = Fixture.Create<string>();
            var count = Fixture.Create<int>();
            var intGenerator = Fixture.Create<Generator<int>>();

            var integers = intGenerator.Take(count).ToArray();
            
            Numbers = $"//[{delimiter}]\n{string.Join(delimiter, integers)}";
            Expected = integers.Sum();

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        [Then]
        public void ReturnsCorrectResult()
        {
            Result.Should().Be(Expected);
        }
    }

    [TestFixture]
    public class AddLineWithMultipleCustomDelimiterStrings : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            var delimiter1 = Fixture.Create<string>();
            var delimiter2 = Fixture.Create<string>();
            var x = Fixture.Create<int>();
            var y = Fixture.Create<int>();
            var z = Fixture.Create<int>();

            Numbers = $"//[{delimiter1}][{delimiter2}]\n{x}{delimiter1}{y}{delimiter2}{z}";
            Expected = x + y + z;

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        [Then]
        public void ReturnsCorrectResult()
        {
            Result.Should().Be(Expected);
        }
    }
}