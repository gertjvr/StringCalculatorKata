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

            var integers = numbersOnly
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .ToList();

            var negatives = integers.Where(i => i < 0).ToArray();
            if (negatives.Any())
                throw new ArgumentOutOfRangeException(
                    "numbers", 
                    string.Format(
                        "Negatives not allowed. Found {0}.",
                        string.Join(",", negatives)));

            return integers.Sum();
        }
    }
}