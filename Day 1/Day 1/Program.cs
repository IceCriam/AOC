using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_1
{
    internal class Program
    {
        static int getValues(string input, bool part2)
        {
            List<string> digits = new List<string>(){ "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            if (part2) for (int i = 1; i < digits.Count; i++) input = input.Replace(digits[i], digits[i][0] + i.ToString() + digits[i][digits[i].Length - 1]);

            int firstIndex = 999999;
            int lastIndex = 0;

            for (int i = 1; i < digits.Count; i++)
            {
                firstIndex = Math.Min(input.IndexOf(i.ToString()) >= 0 ? input.IndexOf(i.ToString()) : 9999999, firstIndex);
                lastIndex = Math.Max(input.LastIndexOf(i.ToString()), lastIndex);
            }

            return int.Parse(input[firstIndex].ToString() + input[lastIndex].ToString());
        }
        static void Main(string[] args)
        {
            int total = 0;
            foreach (string s in new StreamReader("input.txt").ReadToEnd().Split('\n')) total += getValues(s, true);

            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
