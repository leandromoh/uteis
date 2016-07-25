using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        static IEnumerable<char> alfabeto;
        static IEnumerable<char> vogais;
        static IEnumerable<char> consoantes;

        static Program()
        {
            alfabeto = "ABCDEFGHIJKLMNOPQRSTUVQXYZ";
            vogais = "AEIOU";

            alfabeto = concatenaLetrasMinisculas(alfabeto);
            vogais = concatenaLetrasMinisculas(vogais);
            consoantes = alfabeto.Except(vogais);
        }

        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            Console.WriteLine(EmbaralhaPalavra(input));
            Console.WriteLine(RemoveVogais(input));
            Console.WriteLine(RemoveConsoantes(input));

            Console.Read();
        }

        static string EmbaralhaPalavra(string palavra)
        {
            return String.Join("", getRandomNumbers(0, palavra.Length).Select(i => palavra[i]));
        }

        static string RemoveVogais(string palavra)
        {
            return String.Join("", palavra.Where(l => !vogais.Contains(l)));
        }

        static string RemoveConsoantes(string palavra)
        {
            return String.Join("", palavra.Where(l => !consoantes.Contains(l)));
        }

        static IEnumerable<char> concatenaLetrasMinisculas(IEnumerable<char> c)
        {
            return c.Concat(c.Select(s => (char)(s + 32)));
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
