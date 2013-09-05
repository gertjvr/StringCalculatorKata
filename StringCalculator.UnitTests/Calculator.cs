using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.UnitTests
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            int result = 0;
            int.TryParse(numbers, out result);
            return result;
        }
    }
}