using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public static class ExtensionMethods
    {
        public static Tuple<int, IEnumerable<T>> Pagination<T>(this IEnumerable<T> source, int page, int recordsPerPage)
        {
            int records = source.Count();
            int pageCount = (records + recordsPerPage - 1) / recordsPerPage;

            if (page > pageCount)
                throw new Exception("page out of range");

            return Tuple.Create(pageCount, source.Skip((page - 1) * recordsPerPage).Take(recordsPerPage));
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            var res = Enumerable.Range(1, 26).Pagination(1, 5);

            Console.WriteLine(res.Item1 + " Pages Found");

            foreach (var i in res.Item2)
                Console.Write(i + " ");

            Console.Read();
        }
    }
}
