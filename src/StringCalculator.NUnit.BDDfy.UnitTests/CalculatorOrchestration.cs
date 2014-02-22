using System;
using Shouldly;

namespace StringCalculator.NUnit.BDDfy.UnitTests
{
    public class CalculatorOrchestration
    {
        private Calculator Calculator { get; set; }

        private int _result;

        private Exception _exception;

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
            _result.ShouldBe(expectedResult);
        }

        protected void WhenTheResultIsCalculatedThrowsArgumentOutOfRangeException(string input)
        {
            _exception = Should.Throw<ArgumentOutOfRangeException>(() => Calculator.Add(input));
        }

        protected void ThenExceptionMessageStartsWith(string message)
        {
            _exception.Message.ShouldStartWith(message);
        }

        protected void AndExceptionMessageContains(string message)
        {
            _exception.Message.ShouldContain(message);
        }
    }
}