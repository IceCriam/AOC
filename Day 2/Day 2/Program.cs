using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_2
{
    internal class Program
    {
        static int searchColours(string text, bool part2, int gameIndex)
        {
            bool valid = true;

            string[] colours = { "red", "green", "blue" };

            string[] splitText = text.Replace(";", " ;").Split(' ', ',');

            int[] totals = { 0, 0, 0 };

            int[] maxNums = {0, 0, 0 };

            bool readColour = false;

            int num = 0;

            foreach (string s in splitText)
            {
                if (valid || part2)
                {
                    try
                    {
                        num = int.Parse(s);

                        readColour = true;
                    }
                    catch { }

                    if (readColour)
                    {
                        for (int i = 0; i < colours.Length && readColour; i++)
                        {
                            if (s == colours[i])
                            {
                                readColour = false;

                                totals[i] = num;

                                if (num > maxNums[i]) maxNums[i] = num;
                            }
                        }
                    }

                    if (s == ";")
                    {
                        if (totals[0] > 12 || totals[1] > 13 || totals[2] > 14) valid = false;

                        totals = new int[] { 0, 0, 0 };
                    }
                }
                
            }

            if (totals[0] > 12 || totals[1] > 13 || totals[2] > 14) valid = false;

            if (part2)
            {
                return maxNums[0] * maxNums[1] * maxNums[2];
            }

            else if (valid)
            {
                return gameIndex;
            }
            else
            {
                return 0;
            }
        }

        static void Main(string[] args)
        {
            bool part2 = true;

            int total = 0;

            int gameIndex = 1;

            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    total += searchColours(sr.ReadLine(), part2, gameIndex);

                    gameIndex++;
                }
            }

            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
