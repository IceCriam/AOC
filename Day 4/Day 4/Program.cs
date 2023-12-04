using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Day_4
{
    internal class Program
    {
        static int getPoints(string input)
        {
            string[] lines = input.Split('|');
            string[] line1 = lines[0].Split(' ');
            string[] line2 = lines[1].Split(' ');

            int points = 0;

            for (int i = 2; i < line1.Length; i++)
            {
                if (line1[i] != "")
                {
                    if (line2.Contains(line1[i]))
                    {
                        if (points == 0) points = 1;

                        else
                        {
                            points *= 2;
                        }
                    }
                }
            }
            return points;
        }

        static int getWins(string input)
        {
            string[] lines = input.Split('|');
            string[] line1 = lines[0].Split(' ');
            string[] line2 = lines[1].Split(' ');

            int wins = 0;

            for (int i = 2; i < line1.Length; i++)
            {
                if (line1[i] != "")
                {
                    if (line2.Contains(line1[i]))
                    {
                        wins++;
                    }
                }
            }
            return wins;
        }

        static void recurse(List<string> lines, int[] copies, int index, int maxIndex)
        {
            if (index <= maxIndex)
            {
                for (int c = 0; c < copies[index]; c++)
                {
                    int wins = getWins(lines[index]);

                    if (index != maxIndex)
                    {
                        int cap = (index + wins) <= maxIndex ? (index + wins) : maxIndex;

                        for (int j = index + 1; j <= cap; j++)
                        {
                            copies[j]++;
                        }
                    }
                }

                recurse(lines, copies, index + 1, maxIndex);
            }
        } 
        static void Main(string[] args)
        {
            int total = 0;
            bool part2 = true;

            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    if (!part2)
                    {
                        total += getPoints(line);
                    }
                    else
                    {
                        lines.Add(line);
                    }
                }
            }

            if (part2)
            {
                int[] copies = new int[lines.Count];

                for (int i = 0; i < lines.Count; i++)
                {
                    copies[i] = 1;
                }

                recurse(lines, copies, 0, lines.Count - 1);

                foreach (int i in copies)
                {
                    total += i;
                }
            }
            



            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
