using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TestStack.BDDfy;
using TestStack.BDDfy.Scanners.StepScanners.Fluent;

namespace StringCalculator.Fixie.BDDfy.UnitTests
{
    public class CalculatorTests : CalculatorOrchestration
    {
        public void AddEmptyReturnsCorrectResult(Calculator sut)
        {
            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(string.Empty))
                .Then(t => t.ThenTheExpectedResultShouldBe(0))
                .BDDfy();
        }

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

        public void AddAnyAmountOfNumbersReturnsCorrectResult(
            Calculator sut,
            int count,
            IEnumerable<int> generator)
        {
            var intergers = Enumerable.Take<int>(generator, count + 2).ToArray();
            var numbers = string.Join(",", intergers);
            var expected = intergers.Sum();

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        public void AddWithLineBreakAndCommaAsDelimiterReturnsCorrectResult(
            Calculator sut,
            int x,
            int y,
            int z)
        {
            var numbers = string.Format("{0}\n{1},{2}", x, y, z);
            var expected = x + y + z;

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        public void AddLineWithCustomDelimiterReturnsCorrectResult(
            Calculator sut,
            IEnumerable<char> charGenerator,
            int count,
            IEnumerable<int> intGenerator)
        {
            int dummy;
            var delimiter = Enumerable.Where<char>(charGenerator, c => int.TryParse(c.ToString(), out dummy) == false)
                .Where(c => c != '-')
                .First();

            var integers = Enumerable.Take<int>(intGenerator, count).ToArray();
            var numbers = string.Format(
                "//{0}\n{1}",
                delimiter,
                string.Join(delimiter.ToString(), integers));

            var expected = integers.Sum();

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        public void AddLineWithNegativeNumberThrowsCorrectException(
            Calculator sut,
            int x,
            int y,
            int z)
        {
            var numbers = string.Join(",", -x, y, -z);
            var negativeX = (-x).ToString();
            var negativeZ = (-z).ToString();

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculatedThrowsArgumentOutOfRangeException(numbers))
                .Then(t => t.ThenExceptionMessageStartsWith("Negatives not allowed."))
                .And(t => t.AndExceptionMessageContains(negativeX))
                .And(t => t.AndExceptionMessageContains(negativeZ))
                .BDDfy();
        }

        public void AddIgnoresBigNumbersReturnsCorrectResult(
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

        public void AddLineWithCustomDelimiterStringReturnsCorrectResult(
            Calculator sut,
            string delimiter,
            int count,
            IEnumerable<int> intGenerator)
        {
            var integers = Enumerable.Take<int>(intGenerator, count).ToArray();
            var numbers = string.Format(
                "//[{0}]\n{1}",
                delimiter,
                string.Join(delimiter, integers));

            var expected = integers.Sum();

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

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

            var expected = x + y + z;

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }
    }
}