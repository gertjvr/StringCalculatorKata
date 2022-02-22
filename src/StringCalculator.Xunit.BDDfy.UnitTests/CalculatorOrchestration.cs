using System;
using FluentAssertions;
using FluentAssertions.Specialized;

namespace StringCalculator.Xunit.BDDfy.UnitTests
{
    public class CalculatorOrchestration
    {
        public Calculator? Calculator { get; private set; }

        private int _result;

        private ExceptionAssertions<ArgumentOutOfRangeException>? _exception;

        protected void GivenACalculator(Calculator calculator)
        {
            Calculator = calculator;
        }

        protected void WhenTheResultIsCalculated(string input)
        {
            if (Calculator == null) 
                throw new ArgumentNullException(nameof(Calculator));
            
            _result = Calculator.Add(input);
        }

        protected void ThenTheExpectedResultShouldBe(int expectedResult)
        {
            _result.Should().Be(expectedResult);
        }

        protected void WhenTheResultIsCalculatedThrowsArgumentOutOfRangeException(string input)
        {
            if (Calculator == null) 
                throw new ArgumentNullException(nameof(Calculator));
            
            _exception = Calculator.Invoking(_ => _.Add(input)).Should().Throw<ArgumentOutOfRangeException>();
        }

        protected void ThenExceptionMessage(string expectedWildcardPattern)
        {
            if (_exception == null) 
                throw new ArgumentNullException(nameof(_exception));
            
            _exception.WithMessage(expectedWildcardPattern);
        }
    }
}