using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Udsd
{
    class RegexSubstitution
    {
        private string pattern;
        private string replacement;
        private bool isCaseSensitive = false;
        private bool isGlobal = true;

        delegate string RegexReplace(string fileName);
        private RegexReplace regexReplace;
        private Regex regex;
		
        static void Main(string[] args)
        {
            RegexSubstitution regex = new RegexSubstitution(@".*?-", "banda -", false, false);

            Console.WriteLine(regex.replace("12 - aaa.mp4"));
            Console.WriteLine(regex.replace("12.- bbb.mp4"));
            Console.WriteLine(regex.replace("12xdvxdsdfs- ccc.mp4"));
            Console.ReadKey();
        }
		
        public RegexSubstitution(string Pattern, string Replacement, bool CaseSensitive, bool Global)
        {
            isCaseSensitive = CaseSensitive;
            isGlobal = Global;
            pattern = Pattern;
            replacement = Replacement;

            if (!isCaseSensitive)
                pattern = "(?i)" + pattern + "(?-i)";

            if (isGlobal)
                regexReplace = new RegexReplace(RegexReplaceGlobal);
            else
                regexReplace = new RegexReplace(RegexReplaceNotGlobal);

            regex = new Regex(pattern);
        }

        private string RegexReplaceGlobal(string fileName)
        {
            return regex.Replace(fileName, replacement);
        }

        private string RegexReplaceNotGlobal(string fileName)
        {
            return regex.Replace(fileName, replacement, 1);
        }

        public string replace(string fileName)
        {
            return regexReplace(fileName);
        }
    }
}
