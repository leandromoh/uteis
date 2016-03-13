using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication2
{
    class RegexSubstitution
    {
        public string Replacement { get; set; }
        public Regex Regex { get; private set; }
        public Func<string, string> Replace { get; private set; }

        static void Main(string[] args)
        {
            RegexSubstitution regex = new RegexSubstitution(@".*?-", "banda -", false, false);

            Console.WriteLine(regex.Replace("12 - aaa.mp4"));
            Console.WriteLine(regex.Replace("12.- bbb.mp4"));
            Console.WriteLine(regex.Replace("12xdvxdsdfs- ccc.mp4"));
            Console.ReadKey();
        }

        public RegexSubstitution(string Pattern, string Replacement, bool IsCaseSensitive, bool IsGlobal)
        {
            this.Replacement = Replacement;

            RegexOptions Options = RegexOptions.None;

            if (!IsCaseSensitive)
                Options |= RegexOptions.IgnoreCase;

            Regex = new Regex(Pattern, Options);

            if (IsGlobal)
                Replace = fileName => Regex.Replace(fileName, this.Replacement);
            else
                Replace = fileName => Regex.Replace(fileName, this.Replacement, 1);
        }
    }
}
