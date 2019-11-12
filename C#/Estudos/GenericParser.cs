using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    public class GenericParser : IDisposable
    {
        private Section _rootSection;
        private bool _disposed;

        public GenericParser(
            Stream content,
            IEnumerable<(Func<string, bool> startSection, Func<string, bool> endSection)> sectionDelimiters)
        {
            const int defaultBufferSizeInStreamReader = 1024;

            using (var reader = new StreamReader(content, Encoding.UTF8, true, defaultBufferSizeInStreamReader, leaveOpen: true))
            {
                _rootSection = ParseFileToSection("root", reader, sectionDelimiters);
            }
        }

        public IEnumerable<string> GetSectionData(params (int occurrence, string section)[] path)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(GenericParser));

            return GetSectionIn(_rootSection, path);
        }

        public void Dispose()
        {
            _disposed = true;
        }

        private static Section GetSectionIn(Section rootSection, params (int occurrence, string sectionName)[] path)
        {
            return path
                     .Aggregate(rootSection,
                        (acc, pathNode) =>
                           acc.SubSections
                              .Where(x => x.Head == pathNode.sectionName)
                              .ElementAtOrDefault(pathNode.occurrence)
                                  ?? throw new InvalidOperationException($"section '{pathNode.sectionName}' was not found"));
        }

        private static Section ParseFileToSection(
             string sectionName,
             StreamReader source,
             IEnumerable<(Func<string, bool> isStartSection, Func<string, bool> isEndSection)> sectionDelimiters)
        {
            var line = string.Empty;
            var isStartSection = false;
            var isEndSection = false;
            var section = new Section(sectionName);

            while ((line = source.ReadLine()?.Trim()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                sectionDelimiters.Any(del => (isStartSection = del.isStartSection(line)) || (isEndSection = del.isEndSection(line)));

                if (isStartSection)
                {
                    var subSection = ParseFileToSection(line, source, sectionDelimiters);
                    section.SubSections.Add(subSection);
                }
                else if (isEndSection)
                {
                    section.Trailer = line;
                    return section;
                }
                else
                {
                    section.Properties.Add(line);
                }
            }

            return section;
        }
    }

    public class Section : IEnumerable<string>
    {
        public readonly string Head;
        public string Trailer;

        public readonly List<string> Properties = new List<string>();
        public readonly List<Section> SubSections = new List<Section>();

        public Section(string head)
        {
            Head = head;
        }

        public IEnumerator<string> GetEnumerator() => Properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
