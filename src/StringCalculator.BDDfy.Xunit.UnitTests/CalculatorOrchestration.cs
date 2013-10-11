using System;
using Xunit;

namespace StringCalculator.BDDfy.Xunit.UnitTests
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
            Assert.Equal(expectedResult, _result);
        }

        public void WhenTheResultIsCalculatedThrowsArgumentOutOfRangeException(string input)
        {
            _exception = Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.Add(input));
        }

        public void ThenExceptionMessageStartsWith(string message)
        {
            Assert.True(_exception.Message.StartsWith(message));
        }

        public void AndExceptionMessageContains(string message)
        {
            Assert.True(_exception.Message.Contains(message));
        }
    }
}