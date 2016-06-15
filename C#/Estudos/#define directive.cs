#define USEFOREACH

using System;

namespace fummy
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new int[] { 1, 2, 3, 4, 5 };

        #if USEFOREACH
            foreach (var item in items)
            {
        #else
            for(int i = 0; i < items.Length; i++)
            { 
                var item = items[i];
        #endif
                Console.WriteLine(item);
            }
        }
    }
}

