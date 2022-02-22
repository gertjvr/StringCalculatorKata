using System;
using FluentAssertions;
using FluentAssertions.Specialized;
using Xunit;

namespace StringCalculator.Xunit.BDDfy.UnitTests
{
    public class CalculatorOrchestration
    {
        public Calculator Calculator { get; protected set; }

        private int _result;

        private ExceptionAssertions<ArgumentOutOfRangeException> _exception;

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
            Assert.Equal(expectedResult, _result);
        }

        public void WhenTheResultIsCalculatedThrowsArgumentOutOfRangeException(string input)
        {
            _exception = Calculator.Invoking(_ => _.Add(input)).Should().Throw<ArgumentOutOfRangeException>();
        }

        public void ThenExceptionMessage(string expectedWildcardPattern)
        {
            _exception.WithMessage(expectedWildcardPattern);
        }
    }
}