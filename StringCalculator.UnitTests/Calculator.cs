using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.UnitTests
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            return numbers
                .Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .Sum();
        }
    }
}