using System;

namespace CifraDeCesar
{
    class CifraDeCesar
    {
        public static char[] alfabeto = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        static void Main(string[] args)
        {
            string str = String.Join("",alfabeto);
            int chave = -52;

            Console.WriteLine(str);

            string cifrada = encrypt(str, chave);

            Console.WriteLine(cifrada);

            string decifrada = decrypt(cifrada, chave);

            Console.WriteLine(decifrada);

            Console.ReadKey();
        }

        public static string encrypt(string str, int chave)
        {
            if (chave < 0)
                chave = alfabeto.Length + (chave % alfabeto.Length);

            str = str.ToUpper();
            char[] newStr = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                int pos = Array.IndexOf(alfabeto, str[i]);
                if (pos > -1)
                {
                    int newPos = (pos + chave) % alfabeto.Length;
                    newStr[i] = alfabeto[newPos];
                }
                else
                {
                    newStr[i] = str[i];
                }
            }
            return string.Join("", newStr);
        }

        public static string decrypt(string str, int chave)
        {
            if (chave < 0)
                chave = alfabeto.Length + (chave % alfabeto.Length);

            char[] newStr = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                int pos = Array.IndexOf(alfabeto, Char.ToUpper(str[i]));
                if (pos > -1)
                {
                    int newPos = (pos - chave) % alfabeto.Length;

                    if (newPos < 0)
                        newPos = alfabeto.Length + newPos;

                    newStr[i] = alfabeto[newPos];
                }
                else
                {
                    newStr[i] = str[i];
                }
            }
            return string.Join("", newStr);
        }
    }
}
