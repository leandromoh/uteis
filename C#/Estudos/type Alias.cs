using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    using Row = List<string>;
    using Table = List<List<string>>;
    using DataBase = Dictionary<string, List<List<string>>>;

    static class Program
    {
        public static void Main()
        {
            var d = new DataBase();

            d.Add("users", new Table()
                {
                    new Row{ "id", "name"},
                    new Row{ "1", "leandro"},
                    new Row{ "2", "frank"},
                    new Row{ "3", "raul"},
                });

            d.Add("countries", new Table()
                {
                    new Row{ "id", "name"},
                    new Row{ "1", "brazil"},
                    new Row{ "2", "usa"},
                    new Row{ "3", "Peru "},
                });

            printTable(d["users"]);

            Console.WriteLine();

            printTable(d["countries"]);

            Console.Read();
        }

        static void printTable(Table table)
        {
            table.ForEach(row =>
            {
                row.ForEach(column => Console.Write(column + "\t"));
                Console.Write("\n");
            });
        }
    }
}