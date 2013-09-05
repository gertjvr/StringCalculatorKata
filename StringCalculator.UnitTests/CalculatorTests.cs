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
    }
}