using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use composite formatting with format string in the format item.
            Temperature temp1 = new Temperature(0);
            Console.WriteLine("{0:C} (Celsius) = {0:K} (Kelvin) = {0:F} (Fahrenheit)\n", temp1);

            // Use composite formatting with a format provider.
            temp1 = new Temperature(-40);
            Console.WriteLine(String.Format(new CultureInfo("pt-BR"), "{0:C} (Celsius) = {0:K} (Kelvin) = {0:F} (Fahrenheit)\n", temp1));

            // Call ToString method with format string.
            temp1 = new Temperature(32);
            Console.WriteLine("{0} (Celsius) = {1} (Kelvin) = {2} (Fahrenheit)\n", temp1.ToString("C"), temp1.ToString("K"), temp1.ToString("F"));

            // Call ToString with format string and format provider
            temp1 = new Temperature(100);
            NumberFormatInfo nl = NumberFormatInfo.CurrentInfo;
            Console.WriteLine("{0} (Celsius) = {1} (Kelvin) = {2} (Fahrenheit)", temp1.ToString("C", nl), temp1.ToString("K", nl), temp1.ToString("F", nl));

            Console.ReadKey();
        }
    }

    public class Temperature : IFormattable
    {
        private decimal temp;

        public Temperature(decimal temperature)
        {
            if (temperature < -273.15m)
                throw new ArgumentOutOfRangeException(temperature + " is less than absolute zero.");

            this.temp = temperature;
        }

        public decimal Celsius
        {
            get { return temp; }
        }

        public decimal Fahrenheit
        {
            get { return temp * 9 / 5 + 32; }
        }

        public decimal Kelvin
        {
            get { return temp + 273.15m; }
        }

        public override string ToString()
        {
            return this.ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (String.IsNullOrEmpty(format)) format = "G";
            if (provider == null) provider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "G":
                case "C":
                    return Celsius.ToString("F2", provider) + " °C";
                case "F":
                    return Fahrenheit.ToString("F2", provider) + " °F";
                case "K":
                    return Kelvin.ToString("F2", provider) + " K";
                default:
                    throw new FormatException(String.Format("The {0} format string is not supported.", format));
            }
        }
    }
}
