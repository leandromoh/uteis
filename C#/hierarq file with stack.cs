using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static readonly (Func<string, bool>, Func<string, bool>)[] funcs = new (Func<string, bool>, Func<string, bool>)[]
        {
            (x => x == "start file", x => x == "end file"),
            (x => x == "start lote", x => x == "end lote"),
            (x => x == "start header lote", x => x == "end header lote"),
            (x => x == "start body lote", x => x == "end body lote")
        };

        const string text = @"
start file
    dae
    start lote
        start header lote
            record header
        end header lote
        start body lote
            record -1
            record -2
            record -3
        end body lote
    end lote

    dae

    start lote
        start header lote
            record header
        end header lote
        start body lote
            record 1
            record 2
            record 3
        end body lote
        start body lote
            record 10
            record 20
            record 30
            start body lote
                record 100
                record 200
                record 300
            end body lote
            record 40

            start body lote
                record 1000
                record 2000
                record 3000
            end body lote
            record 50

        end body lote
    end lote

end file
";

        static void Main(string[] args)
        {
            Assert_inner_self_contained_section();
            Assert_get_leafs_of_section_with_nested_sections();
            Assert_get_leafs_of_section_is_lazy();

            var file = GetFile();//.Select((x, i) => i == 37 ? throw new Exception() : x);

            var result = GetLeafs(file, new[] {
                (0, "start file"),
                (1, "start lote"),
                (1, "start body lote"),
                (1, "start body lote"),
            }, true, funcs);


            foreach (var i in result)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("fim");
            Console.Read();
        }

        static void Assert_get_leafs_of_section_is_lazy()
        {
            var source = GetFile().Select(x => x == "record 40" ? throw new Exception() : x);

            var result = GetLeafs(source, new[] {
                (0, "start file"),
                (1, "start lote"),
                (1, "start body lote"),
            }, true, funcs);

            var expected = new[]
            {
                "record 10",
                "record 20",
                "record 30",
            };

            if (!result.Select(x => x.Item2).Take(3).SequenceEqual(expected))
                throw new Exception();
        }

        static void Assert_get_leafs_of_section_with_nested_sections()
        {
            var result = GetLeafs(GetFile(), new[] {
                (0, "start file"),
                (1, "start lote"),
                (1, "start body lote"),
            }, true, funcs);

            var expected = new[]
            {
                "record 10",
                "record 20",
                "record 30",
                "record 40",
                "record 50",
            };

            if (!result.Select(x => x.Item2).SequenceEqual(expected))
                throw new Exception();
        }

        static void  Assert_inner_self_contained_section()
        {
            var result = GetLeafs(GetFile(), new[] {
                (0, "start file"),
                (1, "start lote"),
                (1, "start body lote"),
                (1, "start body lote"),
            }, true, funcs);

            var expected = new[]
            {
                "record 1000",
                "record 2000",
                "record 3000",
            };

            if (!result.Select(x => x.Item2).SequenceEqual(expected))
                throw new Exception();
        }

        static public IEnumerable<(int, T)> GetLeafs<T>(
            IEnumerable<T> text,
            (int nth, T func)[] path,
            bool leaf,
            params (Func<T, bool> startSection, Func<T, bool> endSection)[] delimiters)
        {
            var bla = Parse(text, path, delimiters);
            var nivel = path.Length;

            using (var e = bla.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    var isLeaf = e.Current.Item1 == nivel &&
                        !delimiters.Any(x => x.startSection(e.Current.Item2) ||
                                             x.endSection(e.Current.Item2));

                    if (isLeaf == leaf)
                    {
                        yield return e.Current;
                    }
                }
            }
        }

        static public IEnumerable<(int, T)> Parse<T>(
            IEnumerable<T> text,
            (int nth, T func)[] path,
            params (Func<T, bool> startSection, Func<T, bool> endSection)[] delimiters)
        {
            var path2 = path.Select(tuple =>
            {
                Func<T, bool> f = (T item) => Comparer<T>.Default.Compare(item, tuple.func) == 0;
                return (tuple.nth, f);
            });

            return Parse(text, path2.ToArray(), delimiters);
        }

        static public IEnumerable<(int, T)> Parse<T>(
            IEnumerable<T> text,
            (int nth, Func<T, bool> func)[] path,
            params (Func<T, bool> startSection, Func<T, bool> endSection)[] delimiters)
        {
            var identado = identa(text, delimiters);

            using (var e = identado.GetEnumerator())
            {
                var i = -1;

            outerLoop:

                for (i++; i < path.Length;)
                {
                    while (e.MoveNext())
                    {
                        var (nivel, item) = e.Current;

                        if (i == nivel && path[i].func(item))
                        {
                            if (path[i].nth-- == 0)
                            {
                                goto outerLoop;
                            }
                        }

                        else if (nivel < i) //elemento não existe
                        {
                            e.Dispose();
                            yield break;
                        }
                    }
                }

                var (a, b) = e.Current;

                while (e.MoveNext() && e.Current.Item1 > a)
                {
                    yield return (e.Current.Item1, e.Current.Item2);
                }
            }
        }

        static IEnumerable<(int, T)> identa<T>(IEnumerable<T> text,
            params (Func<T, bool> startSection, Func<T, bool> endSection)[] delimiters)
        {
            var stack = new Stack<T>();
            bool startSection = false;
            bool endSection = false;

            foreach (var e in text)
            {
                delimiters.Any(del => (startSection = del.startSection(e)) ||
                                      (endSection = del.endSection(e)));

                if (endSection)
                {
                    stack.Pop();
                }

                yield return (stack.Count, e);

                if (startSection)
                {
                    stack.Push(e);
                }
            }
        }


        static IEnumerable<string> GetFile()
        {
            return text
                .Split('\n', '\r')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x));
        }

    }
}
