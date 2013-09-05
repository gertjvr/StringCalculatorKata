using System;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace StringCalculator.UnitTests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test, CalculatorTestConventions]
        public void AddEmptyReturnsCorrectResults(
            Calculator sut)
        {
            var numbers = "";
            var actual = sut.Add(numbers);
            Assert.AreEqual(0, actual);
        }

        [Test, CalculatorTestConventions]
        public void AddSingleNumberReturnsCorrectResult(
            Calculator sut,
            int expected)
        {
            var numbers = expected.ToString();
            var actual = sut.Add(numbers);
            Assert.AreEqual(expected, actual);
        }

        [Test, CalculatorTestConventions]
        public void AddTwoNumbersReturnsCorrectResult(
            Calculator sut,
            int x,
            int y)
        {
            var numbers = string.Join(",", x, y);
            var actual = sut.Add(numbers);
            Assert.AreEqual(x + y, actual);
        }
    }
}