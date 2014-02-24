using System;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Shouldly;

namespace StringCalculator.SpecFor.Fixie.UnitTests
{
    public abstract class CalculatorSpecFor : AutoSpecFor<Calculator>
    {
        protected string Numbers { get; set; }

        protected int Expected { get; set; }

        protected int Result { get; set; }

        protected CalculatorSpecFor()
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }

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

        public void ReturnsCorrectResult()
        {
            Result.ShouldBe(Expected);
        }
    }

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

        public void ReturnsCorrectResult()
        {
            Result.ShouldBe(Expected);
        }
    }

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

        public void ReturnsCorrectResult()
        {
            Result.ShouldBe(Expected);
        }
    }

    public class AddAnyAmountOfNumbers : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            var generator = Fixture.Create<Generator<int>>();
            var count = Fixture.Create<int>();
            var intergers = generator.Take(count + 2).ToArray();

            Numbers = string.Join(",", intergers);
            Expected = intergers.Sum();

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        public void ReturnsCorrectResult()
        {
            Result.ShouldBe(Expected);
        }
    }

    public class AddWithLineBreakAndCommaAsDelimiter : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            var x = Fixture.Create<int>();
            var y = Fixture.Create<int>();
            var z = Fixture.Create<int>();

            Numbers = string.Format("{0}\n{1},{2}", x, y, z);
            Expected = x + y + z;

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        public void ReturnsCorrectResult()
        {
            Result.ShouldBe(Expected);
        }
    }

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
                .Where(c => c != '-')
                .First();

            var integers = intGenerator.Take(count).ToArray();

            Numbers = string.Format(
                "//{0}\n{1}",
                delimiter,
                string.Join(delimiter.ToString(), integers));

            Expected = integers.Sum();

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        public void ReturnsCorrectResult()
        {
            Result.ShouldBe(Expected);
        }
    }

    public class AddLineWithNegativeNumber : CalculatorSpecFor
    {
        private int x;
        private int y;
        private int z;

        protected override Calculator Given()
        {
            x = Fixture.Create<int>();
            y = Fixture.Create<int>();
            z = Fixture.Create<int>();

            Numbers = string.Join(",", -x, y, -z);

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
        }

        public void ThrowsCorrectException()
        {
            var e = Should.Throw<ArgumentOutOfRangeException>(
                () => Subject.Add(Numbers));

            e.Message.ShouldStartWith("Negatives not allowed.");
            e.Message.ShouldContain((-x).ToString());
            e.Message.ShouldContain((-z).ToString());
        }
    }

    public class AddIgnoresBigNumbers : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            int smallSeed = Fixture.Create<int>();
            int bigSeed = Fixture.Create<int>();

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

        public void ReturnsCorrectResult()
        {
            Result.ShouldBe(Expected);
        }
    }

    public class AddLineWithCustomDelimiterString : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            var delimiter = Fixture.Create<string>();
            var count = Fixture.Create<int>();
            var intGenerator = Fixture.Create<Generator<int>>();

            var integers = intGenerator.Take(count).ToArray();

            Numbers = string.Format(
                "//[{0}]\n{1}",
                delimiter,
                string.Join(delimiter, integers));

            Expected = integers.Sum();

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        public void ReturnsCorrectResult()
        {
            Result.ShouldBe(Expected);
        }
    }

    public class AddLineWithMultipleCustomDelimiterStrings : CalculatorSpecFor
    {
        protected override Calculator Given()
        {
            var delimiter1 = Fixture.Create<string>();
            var delimiter2 = Fixture.Create<string>();
            var x = Fixture.Create<int>();
            var y = Fixture.Create<int>();
            var z = Fixture.Create<int>();

            Numbers = string.Format(
                "//[{0}][{1}]\n{2}{0}{3}{1}{4}",
                delimiter1,
                delimiter2,
                x,
                y,
                z);

            Expected = x + y + z;

            return Fixture.Create<Calculator>();
        }

        protected override void When()
        {
            Result = Subject.Add(Numbers);
        }

        public void ReturnsCorrectResult()
        {
            Result.ShouldBe(Expected);
        }
    }
}
