using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new[]
            {
                new {word = "bannanas", guess = "n"}, //[2,3,5]
                new {word = "test test test test", guess = "test"}, //[0,5,10,15]
                new {word = "Hello world, welcome to the universe.", guess = "o"} //[4,7,17,22]
            };

            foreach (var obj in array)
            {
                Console.WriteLine(String.Join(" ", indexs(obj.word, obj.guess)));

                int j = index(obj.word, obj.guess);
                while (j >= 0)
                {
                    Console.Write(j + " ");
                    j = index(obj.word, obj.guess, j + 1);
                }

                Console.WriteLine();
            }

            Console.Read();
        }

        static int[] indexs(string x, string y)
        {
            List<int> result = new List<int>();
            var i = 0;
            var j = 0;
            var k = 0;

            while (true)
            {
                i = index(x, y);
                if (i < 0)
                {
                    break;
                }
                else
                {
                    result.Add(i + j);
                    k = i + y.Length;
                    x = x.Substring(k);
                    j += k;
                }
            };

            return result.ToArray();
        }

        static int index(string x, string y, int startPosition = 0)
        {
            var len1 = x.Length;
            var len2 = y.Length;
            var maxIndex = 0;
            var j = 0;
            var k = 0;

            if (startPosition >= len1 || len1 < len2)
                return -1;
            else
                maxIndex = len1 - len2;

            for (; startPosition <= maxIndex; startPosition++)
            {
                for (k = startPosition, j = 0; j < len2; j++, k++)
                {
                    if (x[k] != y[j])
                    {
                        break;
                    }
                }
                if (j == len2)
                {
                    return startPosition;
                }
            }

            return -1;
        }
    }
}
