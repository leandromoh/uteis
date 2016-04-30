using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var n in getRandomNumbers(0, 10))
            {
                Console.Write(n + " ");
            }

            Console.Read();
        }
		
        static List<int> getRandomNumbers(int listSize, int minValue, int maxValue)
        {
            Random rand = new Random();
            HashSet<int> check = new HashSet<int>();
            int curValue;

            for (int i = 0; i < listSize; i++)
            {
                do
                {
                    curValue = rand.Next(minValue, maxValue);
                } while (check.Contains(curValue));

                check.Add(curValue);
            }

            return check.ToList();
        }

        static List<int> getRandomNumbers(int start, int count)
        {
            Random rand = new Random();
            List<int> result = new List<int>(Enumerable.Range(start, count));
            int j = 0, temp = 0;

            for (int i = 0; i < result.Count; i++)
            {
                j = rand.Next(0, count);
                temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            return result;
        }
    }
}