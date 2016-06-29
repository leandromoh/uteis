using System;
using System.Globalization;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Format(new DoubleFormatter(), "Width = {0} \nHeight = {1}", 10.34556, 11.23476));

            Console.ReadKey();
        }
    }

    public class DoubleFormatter : IFormatProvider, ICustomFormatter
    {
        // always use dot separator for doubles
        private CultureInfo enUsCulture = CultureInfo.GetCultureInfo("en-US");

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            // format doubles to 3 decimal places
            return string.Format(enUsCulture, "{0:000.000}", arg);
        }

        public object GetFormat(Type formatType)
        {
            return (formatType == typeof(ICustomFormatter)) ? this : null;
        }
    }
}