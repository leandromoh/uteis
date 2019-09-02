using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class GenericParserService
    {
        readonly IEnumerable<object> _tree;

        public GenericParserService(
            string text,
            params (Func<string, bool> startSection, Func<string, bool> endSection)[] sections)
        {
            var lines = text
                .Split('\n', '\r')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x));

            _tree = UnFlatten(
                        lines,
                        (startNode, sons, endNode) => (startNode, sons, endNode),
                        sections)
                   .ToArray();
        }

        public string[] GetLeafsOf(params (int nth, string node)[] path)
        {
            return GetNodesOf(path).OfType<string>().ToArray();
        }

        public (string parent, IEnumerable<object> sons)[] GetSectionsOf(params (int nth, string node)[] path)
        {
            return GetNodesOf(path).OfType<(string parent, IEnumerable<object> sons)>().ToArray();
        }

        IEnumerable<object> GetNodesOf(params (int nth, string node)[] paths)
        {
            return paths
                     .Aggregate(_tree,
                        (acc, path) =>
                           acc.OfType<(string startSectionNode, IEnumerable<object> sons, string endSectionNode)>()
                              .Where(x => x.startSectionNode == path.node)
                              .ElementAt(path.nth).sons);
        }

        static IEnumerable<object> UnFlatten<T, TResult>(
            IEnumerable<T> source,
            Func<T, IEnumerable<object>, T, TResult> selector,
            params (Func<T, bool> startSection, Func<T, bool> endSection)[] sections)
        {
            var e = source.GetEnumerator();

            return disposes();

            IEnumerable<object> disposes()
            {
                try
                {
                    foreach (var item in enumerates())
                        yield return item;
                }
                finally
                {
                    e.Dispose();
                }

                IEnumerable<object> enumerates(Func<T, bool> endSection = null)
                {
                    while (e.MoveNext())
                    {
                        if (endSection?.Invoke(e.Current) ?? false)
                            yield break;

                        var func = sections.FirstOrDefault(x => x.startSection(e.Current));

                        if (func != default)
                        {
                            var startSectionNode = e.Current;
                            var children = enumerates(func.endSection).ToArray();
                            var endSectionNode = e.Current;

                            yield return selector(startSectionNode, children, endSectionNode);
                        }
                        else
                        {
                            yield return e.Current;
                        }
                    }
                }
            }
        }
    }
}
