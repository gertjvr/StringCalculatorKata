using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.UnitTests
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            var delimiters = new[] {",", "\n"};
            var numbersOnly = numbers;

            if (numbers.StartsWith("//"))
            {   
                delimiters = new[] { numbers.Skip(2).First().ToString() };
                numbersOnly = new string(numbers.SkipWhile(c => c != '\n').ToArray());
            }

            return numbersOnly
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .Sum();
        }
    }
}