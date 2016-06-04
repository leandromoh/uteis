using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var domainPerson = new DomainPerson { Id = "A", FirstName = "Tore" , LastName = "TheCoder" };
            var guiPerson = new GuiPerson() { Id = 1, LastName = "x" };

            Mapping.Map(domainPerson, guiPerson, excludedProperties: new[] { "LastName" });

            Console.WriteLine(guiPerson);

            Console.Read();
        }
    }

    static class Mapping
    {
        public static void Map<TIn, TOut>(TIn input, TOut output, ICollection<string> includedProperties = null, ICollection<string> excludedProperties = null)
            where TIn : class
            where TOut : class
        {
            if (input == null || output == null)
                return;

            Type inType = input.GetType();
            Type outType = output.GetType();

            bool includedNotNull = includedProperties != null;
            bool excludedNotNull = excludedProperties != null;

            foreach (PropertyInfo info in inType.GetProperties())
            {
                PropertyInfo outfo = ((info != null) && info.CanRead)
                    ? outType.GetProperty(info.Name, info.PropertyType)
                    : null;

                if (outfo != null && outfo.CanWrite && (outfo.PropertyType.Equals(info.PropertyType)))
                {
                    if (excludedNotNull && excludedProperties.Contains(info.Name))
                        continue;

                    if (includedNotNull && includedProperties.Contains(info.Name))
                        outfo.SetValue(output, info.GetValue(input, null), null);
                    else if (!includedNotNull)
                        outfo.SetValue(output, info.GetValue(input, null), null);
                }
            }
        }
    }

    public class GuiPerson
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public override string ToString()
        {
            return string.Format("{{ Id = {2}, FirstName = {0}, LastName = {1} }}", FirstName, LastName, Id);
        }
    }

    public class DomainPerson
    {
        public string Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }
}
