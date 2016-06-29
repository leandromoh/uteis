using System;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            font: https://www.careercup.com/question?id=5767203879124992

            You are given a matrix with N rows and N columns. 
            Elements in matrix can be either 1 or 0. 
            Each row and column of matrix is sorted in ascending order. 
            Find number of 0-s in the given matrix. 
            Example:


            0 0 1
            0 1 1
            1 1 1
            Answer: 3

            0 0
            0 0
            Answer: 4

            */
            Console.WriteLine(countZeros(new int[,] {{0, 0, 1},
                                                     {0, 1, 1},
                                                     {1, 1, 1}}) == 3);

            Console.WriteLine(countZeros(new int[,] {{0, 0},
                                                     {0, 0}}) == 4);

            Console.WriteLine(countZeros(new int[,] {{1, 1, 1, 1},
                                                     {1, 1, 1, 1},
                                                     {1, 1, 1, 1},
                                                     {1, 1, 1, 1}}) == 0);

            Console.WriteLine(countZeros(new int[,] {{0, 0, 0, 0},
                                                     {0, 0, 0, 1},
                                                     {1, 1, 1, 1},
                                                     {1, 1, 1, 1}}) == 7);

            Console.WriteLine(countZeros(new int[,] { { 0 } }) == 1);

            Console.WriteLine(countZeros(new int[,] { { 1 } }) == 0);

            Console.ReadKey();
        }

        static int countZeros(int[,] m)
        {
            int n = (int) Math.Sqrt(m.Length);
            int n2 = n - 1;
            int iLimit = n;
            int jLimit = n;
            int soma = 0;

            for (int i = 0; i < iLimit && m[i, 0] != 1; i++)
            {
                if (m[i, n2] == 0)
                {
                    soma += n;
                    continue;
                }

                for (int j = 0; j < jLimit; j++)
                {
                    if (m[i, j] == 1)
                    {
                        soma += j;
                        jLimit = j;
                        break;
                    }
                }
            }

            return soma;
        }
    }
}
