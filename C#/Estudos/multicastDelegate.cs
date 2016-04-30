using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

public class DelegateFun
{
    public delegate void Escreve();

    static void Main()
    {
        Escreve e1 = new Escreve(um);
        Escreve e2 = dois; //alternative syntaxe, called: method group conversion

        e1(); // um
        e2(); // dois
        Console.WriteLine(String.Empty);
        e1 += tres;
        e1(); // um tres
        Console.WriteLine(String.Empty);
        e2 += quatro;
        e2(); //dois quatro
        Console.WriteLine(String.Empty);

        Escreve e3 = new Escreve(cinco); // cinco
        e3 += e1; // cinco um tres
        e3 += e2; // cinco um tres dois quatro
        e3 += um; // cinco um tres dois quatro um
        e3();

        Console.WriteLine("A");

        Escreve e4 = e1 + e2 - um + cinco; // = um tres + dois quatro - um + cinco
        e4();

        Console.Read();
    }

    static void um()
    {
        Console.WriteLine("-1-");
    }
    static void dois()
    {
        Console.WriteLine("-2-");
    }
    static void tres()
    {
        Console.WriteLine("-3-");
    }
    static void quatro()
    {
        Console.WriteLine("-4-");
    }

    static void cinco()
    {
        Console.WriteLine("-5-");
    }
}
