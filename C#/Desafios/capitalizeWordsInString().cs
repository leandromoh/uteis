using System;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static string capitalizeWord(string str)
    {
        if (new Regex("^[IVXLCDM]+$").IsMatch(str))
            return str.ToUpper();

        if (str.Length > 3)
            return Char.ToUpper(str[0]) + str.Substring(1).ToLower();
        
        return str.ToLower();
    }

    static string capitalizeWordsInString(string str) => String.Join(" ", str.Split(' ').Select(capitalizeWord));

    static void Main()
    {
        var x = new string[] {
            "ALGORÍTIMOS E LÓGICA DE PROGRAMAÇÃO",
            "LINGUAGEM DE PROGRAMAÇÃO",
            "LINGUAGEM DE PROGRAMAÇÃO IV",
            "LABORATÓRIO DE BANCO DE DADOS",
            "ESTRUTURAS DE DADOS",
            "ENGENHARIA DE SOFTWARE III",
            "PROGRAMAÇÃO ORIENTADA A OBJETOS"
        };

        var y = x.Select(capitalizeWordsInString).ToArray();
        
        var y = x.Select(S => String.Join(" ", S.Split(' ').Select(s => (new Regex("^[IVXLCDM]+$").IsMatch(s)) ? s.ToUpper() : (s.Length > 3) ? Char.ToUpper(s[0]) + s.Substring(1).ToLower() : s.ToLower()))).ToArray();
    }
}
