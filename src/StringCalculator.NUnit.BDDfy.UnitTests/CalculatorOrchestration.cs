using System;
using FluentAssertions;
using FluentAssertions.Specialized;
using NUnit.Framework;

namespace StringCalculator.NUnit.BDDfy.UnitTests
{
    public class CalculatorOrchestration
    {
        public Calculator Calculator { get; protected set; }

        private int _result;

        private ExceptionAssertions<ArgumentOutOfRangeException> _exception;

        protected void GivenACalculator(Calculator calculator)
        {
            Calculator = calculator;
        }

        protected void WhenTheResultIsCalculated(string input)
        {
            _result = Calculator.Add(input);
        }

        protected void ThenTheExpectedResultShouldBe(int expectedResult)
        {
            _result.Should().Be(expectedResult);
        }

        protected void WhenTheResultIsCalculatedThrowsArgumentOutOfRangeException(string input)
        {
            _exception = Calculator.Invoking(_ => _.Add(input)).Should().Throw<ArgumentOutOfRangeException>();
        }

        public void ThenExceptionMessage(string expectedWildcardPattern)
        {
            _exception.WithMessage(expectedWildcardPattern);
        }
    }
}