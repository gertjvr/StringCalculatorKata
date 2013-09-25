using System;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.NUnit2;
using TestStack.BDDfy;
using TestStack.BDDfy.Scanners.StepScanners.Fluent;

namespace StringCalculator.UnitTests
{
    public class CalculatorOrchestration
    {
        public Calculator Calculator { get; protected set; }

        private int _result;
        private Exception _exception;

        public void GivenACalculator(Calculator calculator)
        {
            Calculator = calculator;
        }

        public void WhenTheResultIsCalculated(string input)
        {
            _result = Calculator.Add(input);
        }

        public void ThenTheExpectedResultShouldBe(int expectedResult)
        {
            Assert.AreEqual(expectedResult, _result);
        }

        public void WhenTheResultIsCalculatedThrowsArgumentOutOfRangeException(string input)
        {
            _exception = Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Add(input));
        }

        public void ThenExceptionMessageStartsWith(string message)
        {
            Assert.IsTrue(_exception.Message.StartsWith(message));
        }

        public void AndExceptionMessageContains(string message)
        {
            Assert.IsTrue(_exception.Message.Contains(message));
        }
    }

    [TestFixture]
    public class CalculatorBDDfyTests : CalculatorOrchestration
    {
        [Test, CalculatorTestConventions]
        public void AddEmptyReturnsCorrectResults(Calculator sut)
        {   
            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(""))
                .Then(t => t.ThenTheExpectedResultShouldBe(0))
                .BDDfy();
        }

        [Test, CalculatorTestConventions]
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

        [Test, CalculatorTestConventions]
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

        [Test, CalculatorTestConventions]
        public void AddAnyAmountOfNumbersReturnsCorrectResult(
            Calculator sut,
            int count,
            Generator<int> generator)
        {
            var intergers = generator.Take(count + 2).ToArray();
            var numbers = string.Join(",", intergers);
            var expected = intergers.Sum();

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        [Test, CalculatorTestConventions]
        public void AddWithLineBreakAndCommaAsDelimiterRetunrsCorrectResult(
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

            var expected = integers.Sum();

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }

        [Test, CalculatorTestConventions]
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

        [Test, CalculatorTestConventions]
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

            var expected = integers.Sum();

            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
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

            var expected = x + y + z; 
            
            this.Given(t => t.GivenACalculator(sut))
                .When(t => t.WhenTheResultIsCalculated(numbers))
                .Then(t => t.ThenTheExpectedResultShouldBe(expected))
                .BDDfy();
        }
    }
}