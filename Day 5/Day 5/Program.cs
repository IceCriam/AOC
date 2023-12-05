using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_5
{
    internal class Program
    {
        static void compareMaps(List<string>[] maps, List<int[]>[] ordinates, int currentIndex, int nextIndex)
        {
            //If nStart < cStart + cRange
            //If nStart + nRange >= cStart

            //if nStart > cStart,  ordinate = nStart
            //else if nStart <= cStart,  ordinate = cstart

            for (int i = 0; i < maps[currentIndex].Count; i++)
            {
                string[] currentLine = maps[currentIndex][i].Split(' ');

                int cStart = int.Parse(currentLine[1]);
                int cRange = int.Parse(currentLine[2]);

                for (int j = 0; j < maps[nextIndex].Count; j++)
                {
                    string[] nextLine = maps[nextIndex][j].Split(' ');

                    int nStart = int.Parse(nextLine[0]);
                    int nRange = int.Parse(nextLine[2]);

                    if (cStart < (nStart + nRange) && (cStart + cRange) >= nStart)
                    {
                        if (nStart > cStart)
                        {
                            ordinates[currentIndex][i][0] = nStart;
                        }

                        else
                        {
                            ordinates[currentIndex][i][0] = cStart;
                        }

                        ordinates[currentIndex][i][1] = j;
                    }
                }
            }
        }

        static void compareFinal(List<string>[] maps, List<int[]>[] ordinates,string[] seeds)
        {
            int index = 0;

            foreach (string line in maps[0])
            {
                string[] currentLine = line.Split();

                int cStart = int.Parse(currentLine[0]);
                int cRange = int.Parse(currentLine[2]);

                for (int seedIndex = 1; seedIndex < seeds.Length; seedIndex++)
                {
                    string seed = seeds[seedIndex];

                    int seedOrdinate = int.Parse(seed);

                    if (seedOrdinate > cStart && seedOrdinate < (cStart + cRange))
                    {
                        ordinates[0][index][0] = seedOrdinate;
                        ordinates[0][index][1] = seedIndex;
                    }
                }

                index++;
            }
        }

        static bool linksToSeed(List<int[]>[] ordinates, int thisMap, int thisIndex)
        {
            int nextIndex = ordinates[thisMap][thisIndex][1];
            if (thisMap == 0)
            {
                if (nextIndex != -1)
                {
                    return true;
                }

                return false;
            }
            if (nextIndex != -1)
            {
                Console.WriteLine(ordinates[thisMap][thisIndex][0] + ", " + nextIndex);
                return linksToSeed(ordinates, thisMap - 1, nextIndex);
            }

            return false;
        }

        static void Main(string[] args)
        {
            string[] seeds;

            List<string>[] maps = new List<string>[7]
            { new List<string>(), 
              new List<string>(), 
              new List<string>(),
              new List<string>(),
              new List<string>(), 
              new List<string>(), 
              new List<string>() 
            };

            List<int[]>[] ordinates = new List<int[]>[7]
            { new List<int[]>(),
              new List<int[]>(),
              new List<int[]>(),
              new List<int[]>(),
              new List<int[]>(),
              new List<int[]>(),
              new List<int[]>()
            };

            using (StreamReader sr = new StreamReader("test.txt"))
            {
                seeds = sr.ReadLine().Split(' ');

                int index = -1;

                while (!sr.EndOfStream)
                {
                    int lineIndex = 0;

                    string line = sr.ReadLine();


                    if (line == "")
                    {

                        sr.ReadLine();
                        index++;
                        lineIndex = 0;
                    }
                    else
                    {
                        maps[index].Add(line);
                        ordinates[index].Add(new int[2] { 999999999, -1});
                        lineIndex++;
                    }
                }
            }

            for (int i = maps.Length - 1; i > 0; i--)
            {
                compareMaps(maps, ordinates, i, i - 1);
            }

            compareFinal(maps, ordinates, seeds);

            int lowestOrdinate = 8888888;

            int ordCount = ordinates.Length - 1;

            for (int i = 0; i < ordinates[ordCount].Count; i++)
            {
                int nextIndex = ordinates[ordCount][i][1];

                if (nextIndex != -1)
                {
                    if (linksToSeed(ordinates, ordCount - 1, nextIndex))
                    {
                        lowestOrdinate = Math.Min(ordinates[ordCount][i][0], lowestOrdinate);
                    }
                }
            }

            Console.WriteLine(lowestOrdinate);
            Console.ReadKey();
        }
    }
}
