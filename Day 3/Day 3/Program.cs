using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Schema;

namespace Day_3
{
    internal class Program
    {
        static int getTotal(List<string> schem, bool part2)
        {
            string valids = "0123456789";

            int total = 0;

            List<int[]> gears = new List<int[]>();

            for (int y = 0; y < schem.Count; y++)
            {
                for (int x = 0; x < schem[y].Length; x++)
                {
                    char Char = schem[y][x];
                    
                    if (valids.Contains(Char))
                    {
                        if (part2)
                        {
                            int[] gearLocation = searchGear(schem, x, y);

                            if (gearLocation[0] != -1 && gearLocation[1] != -1)
                            {
                                string left = searchLeft(schem, x, y);
                                string middle = Char.ToString();
                                string right = searchRight(schem, ref x, y);
                                int num = int.Parse(left + middle + right);

                                gears.Add(new int[] {gearLocation[0], gearLocation[1], num });
                            }
                        }
                        else
                        {
                            if (searchSymbol(schem, x, y))
                            {

                                string left = searchLeft(schem, x, y);
                                string middle = Char.ToString();
                                string right = searchRight(schem, ref x, y);
                                string num = left + middle + right;

                                total += int.Parse(num);
                            }
                        }
                    }
                }
            }

            if (part2)
            {
                for (int i = 0; i < gears.Count; i++)
                {
                    int numOfGears = 1;

                    int gearTotal = gears[i][2];

                    for (int j = i;  j < gears.Count; j++)
                    {
                        if (i != j)
                        {
                            if (gears[i][0] == gears[j][0] && gears[i][1] == gears[j][1])
                            {
                                numOfGears++;

                                gearTotal *= gears[j][2];

                                gears.RemoveAt(j);
                            }
                        }
                    }

                    if (numOfGears == 2) total += gearTotal;
                }
            }

            return total;
        }

        static int[] searchGear(List<string> schem, int x, int y)
        {
            for (int i = ((x - 1) >= 0) ? x - 1 : x; i <= (x + 1 < schem[0].Length ? x + 1 : x); i++)
            {
                for (int j = ((y - 1) >= 0) ? y - 1 : y; j <= (y + 1 < schem.Count() ? y + 1 : y); j++)
                {
                    if (schem[j][i] == '*')
                    {
                        return new int[]{j, i};
                    }
                }
            }

            return new int[] { -1, -1 };
        }

        static bool searchSymbol(List<string> schem, int x, int y)
        {
            string invalids = "01234566789.";

            for (int i = ((x - 1) >= 0) ? x - 1 : x; i <= (x + 1 < schem[0].Length ? x + 1 : x); i++)
            {
                for (int j = ((y - 1) >= 0) ? y - 1 : y; j <= (y + 1 < schem.Count() ? y + 1 : y); j++)
                {
                    if (!invalids.Contains(schem[j][i]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static string searchLeft(List<string> schem, int x, int y)
        {
            string nums = "01234566789";

            char currentChar;

            string num = "";

            do
            {
                x--;
                if (x < 0) currentChar = '.';

                else
                {
                    currentChar = schem[y][x];
                }
                
                if (nums.Contains(currentChar)) num = currentChar + num;
            }
            while (nums.Contains(currentChar));

            return num;
        }

        static string searchRight(List<string> schem, ref int x, int y)
        {
            string nums = "0123456789";

            char currentChar;

            string num = "";

            do
            {
                x++;

                if (x >= schem[y].Length) currentChar = '.';

                else
                {
                    currentChar = schem[y][x];
                }

                if (nums.Contains(currentChar)) num = num + currentChar;
            }
            while (nums.Contains(currentChar));

            x--;

            return num;
        }
        static void Main(string[] args)
        {
            List<string> schem = new List<string>();

            using (StreamReader sr = new StreamReader("input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    schem.Add(sr.ReadLine());
                }
            }

            Console.WriteLine(getTotal(schem, true));
            Console.ReadKey();
        }
    }
}
