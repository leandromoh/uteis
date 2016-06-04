using System;
using System.Collections.Generic;
using System.Collections;

namespace ConsoleApplication1
{
    class SummableIntList : IEnumerable<int>
    {
        private readonly List<int> list;

        public SummableIntList()
        {
            list = new List<int>();
        }

        public static SummableIntList operator +(SummableIntList x, SummableIntList y)
        {
            List<int> ax = x.list;
            List<int> ay = y.list;
            List<int> ar = null;

            var min = Math.Min(ax.Count, ay.Count);

            var r = new SummableIntList();
            ar = r.list;

            for (int i = 0; i < min; i++)
            {
                ar.Add(ax[i] + ay[i]);
            }

            return r;
        }

        public static SummableIntList operator +(SummableIntList x, int y)
        {
            var r = new SummableIntList();
            var ar = r.list;
            var ax = x.list;

            for (int i = 0; i < ax.Count; i++)
            {
                ar.Add(ax[i] + y);
            }

            return r;
        }

        public static SummableIntList operator -(SummableIntList x)
        {
            var r = new SummableIntList();
            var ar = r.list;
            var ax = x.list;

            for (int i = 0; i < ax.Count; i++)
            {
                ar.Add(-ax[i]);
            }

            return r;
        }

        #region Code Necessary To Support Collection Initializers

        public void Add(int x)
        {
            list.Add(x);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    class Program
    {
        public static void Main(string[] args)
        {
            var a = new SummableIntList() { 1, 2, 3 };
            var b = new SummableIntList() { 5, 3, 4 };

            printArray(a + b); // 6 5 7
            printArray(a + 5); // 6 7 8
            printArray(-a); //-1 -2 -3

            a += -a; //a = a + -a
            printArray(a); //0 0 0

            Console.Read();
        }

        private static void printArray<T>(T a)
            where T : IEnumerable
        {
            foreach (var i in a)
                Console.Write(i + " ");

            Console.Write("\n");
        }
    }
}
