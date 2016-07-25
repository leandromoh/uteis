using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Linq;
using System.Globalization;

namespace ConsoleApplication1
{
    class Counter
    {
        ConcurrentDictionary<string, int> _wordCounts =
          new ConcurrentDictionary<string, int>();

        public Action<DirectoryInfo> ProcessDirectory()
        {
            return (dirInfo =>
            {
                var files = dirInfo.GetFiles("*.cs").AsParallel<FileInfo>();
                files.ForAll<FileInfo>(
                  fileInfo =>
                  {
                      var fileContent = File.ReadAllText(fileInfo.FullName);
                      var sb = new StringBuilder();
                      foreach (var val in fileContent)
                      {
                          sb.Append(char.IsLetter(val) ? val.ToString().ToLowerInvariante() : " ");
                      }
                      string[] wordsInFile = sb.ToString().Split(new[] { ' ' },
                       StringSplitOptions.RemoveEmptyEntries);
                      foreach (var word in wordsInFile)
                      {
                          _wordCounts.AddOrUpdate(word, 1, (s, n) => n++ );
                      }
                  });
                var directories = dirInfo.GetDirectories().AsParallel<DirectoryInfo>();
                directories.ForAll<DirectoryInfo>(ProcessDirectory());
            });
        }
    }
}
