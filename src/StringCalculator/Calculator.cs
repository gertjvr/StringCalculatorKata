using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            var delimiters = new[] {",", "\n"};
            var numbersOnly = numbers;

            if (numbers.StartsWith("//"))
            {
                if (numbers.StartsWith("//["))
                    delimiters = GetDelimiters(numbers);
                else
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
                    nameof(numbers),
                    $"Negatives not allowed. Found {string.Join(",", negatives)}.");

            return integers.Where(i => i <= 1000).Sum();
        }

        private static string[] GetDelimiters(string numbers)
        {
            var envelop = numbers.Substring(2, numbers.IndexOf("]\n", StringComparison.Ordinal) - 1);
            var s = envelop;
            var delimiters = new List<string>();
            while (s != string.Empty)
            {
                var closingBracketPosition = s.IndexOf("]", StringComparison.Ordinal);
                delimiters.Add(s.Substring(1, closingBracketPosition - 1));
                s = s.Substring(closingBracketPosition + 1);
            }

            return delimiters.ToArray();
        }
    }
}