// adapted from https://stackoverflow.com/a/67138189

using System;

var f = Sum(Console.WriteLine);

f(2)(5)(10)(3);

Func<int, Func<int, dynamic>> Sum(Action<int> callback)
{
    return Magic;
    
    Func<int, dynamic> Magic(int a) 
    {
        callback(a);
        return b => Magic(a + b);
    }
}

